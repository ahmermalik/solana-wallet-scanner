using WalletScanner.Models;
using WalletScanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WalletScanner.Repositories
{
    public class WalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet> GetWalletByAddress(string address)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.Address == address);
        }

        public async Task AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
