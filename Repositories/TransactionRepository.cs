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

        public async Task<List<Transaction>> GetTransactionsForWallet(string walletAddress)
        {
            return await _context.Transactions
                .Where(t => t.Wallet.Address == walletAddress)
                .ToListAsync();
        }
    }
}
