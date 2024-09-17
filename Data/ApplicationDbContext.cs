using Microsoft.EntityFrameworkCore;
using WalletScanner.Models;

namespace WalletScanner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<WhaleActivity> WhaleActivities { get; set; } // If needed
    }
}
