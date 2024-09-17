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
        public DbSet<WhaleActivity> WhaleActivities { get; set; }
        public DbSet<DumpEvent> DumpEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>()
                .HasIndex(w => w.Address)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId);

            modelBuilder.Entity<WhaleActivity>()
                .HasIndex(wa => new { wa.Token, wa.WalletAddress, wa.Timestamp });

            modelBuilder.Entity<Alert>()
                .HasIndex(a => new { a.Token, a.AlertType });

            modelBuilder.Entity<DumpEvent>()
                .HasNoKey(); // Since DumpEvent is a result of a stored procedure, it may not have a primary key
        }
    }
}
