using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Data;
using WalletScanner.Models;

namespace WalletScanner.Repositories
{
    public class WalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WalletHolding> GetWalletHoldingsByToken(string tokenSymbol, int networkId)
        {
            return _context.WalletHoldings
                .Include(wh => wh.Token)
                .Include(wh => wh.Wallet)
                .Where(wh =>
                    wh.Token != null &&
                    wh.Token.Symbol == tokenSymbol &&
                    wh.Token.NetworkId == networkId
                )
                .ToList();
        }

        // Additional method to get wallet by address and network
        public Wallet GetWalletByAddress(string walletAddress, int networkId)
        {
            return _context.Wallets
                .Include(w => w.Network)
                .FirstOrDefault(w => w.Address == walletAddress && w.NetworkId == networkId);
        }
    }
}
