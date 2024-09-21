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

        public List<WalletHolding> GetWalletHoldingsByToken(string tokenSymbol)
        {
            return _context
                .WalletHoldings.Include(wh => wh.Token)
                .Where(wh => wh.Token != null && wh.Token.Symbol == tokenSymbol)
                .ToList();
        }
    }
}
