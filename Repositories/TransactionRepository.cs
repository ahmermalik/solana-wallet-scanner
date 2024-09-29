using WalletScanner.Models;
using WalletScanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletScanner.Repositories
{
    public class TransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsForWalletAsync(string walletAddress, int networkId)
        {
            return await _context.Transactions
                .Include(t => t.FromWallet)
                .Include(t => t.ToWallet)
                .Include(t => t.Token)
                .Include(t => t.Network)
                .Where(t =>
                    (t.FromWallet != null && t.FromWallet.Address == walletAddress && t.FromWallet.NetworkId == networkId) ||
                    (t.ToWallet != null && t.ToWallet.Address == walletAddress && t.ToWallet.NetworkId == networkId)
                )
                .ToListAsync();
        }
    }
}
