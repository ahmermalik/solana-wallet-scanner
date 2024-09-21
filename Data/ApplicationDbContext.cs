using Microsoft.EntityFrameworkCore;
using WalletScanner.Models;

namespace WalletScanner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<WalletHolding> WalletHoldings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<WhaleActivity> WhaleActivities { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DumpEvent> DumpEvents { get; set; }
        public DbSet<WalletMetric> WalletMetrics { get; set; }
        public DbSet<TokenMetric> TokenMetrics { get; set; }
        public DbSet<TrendingToken> TrendingTokens { get; set; }
        public DbSet<TopTrader> TopTraders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Wallet
            modelBuilder.Entity<Wallet>().HasIndex(w => w.Address).IsUnique();

            // Token
            modelBuilder.Entity<Token>().HasIndex(t => t.Address).IsUnique();

            // WalletHoldings
            modelBuilder.Entity<WalletHolding>().HasKey(wh => new { wh.WalletId, wh.TokenId });

            modelBuilder
                .Entity<WalletHolding>()
                .HasOne(wh => wh.Wallet)
                .WithMany(w => w.WalletHoldings)
                .HasForeignKey(wh => wh.WalletId);

            modelBuilder
                .Entity<WalletHolding>()
                .HasOne(wh => wh.Token)
                .WithMany(t => t.WalletHoldings)
                .HasForeignKey(wh => wh.TokenId);

            // Transaction
            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId);

            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.Token)
                .WithMany(tk => tk.Transactions)
                .HasForeignKey(t => t.TokenId);

            modelBuilder.Entity<Transaction>().HasIndex(t => t.WalletId);

            modelBuilder.Entity<Transaction>().HasIndex(t => t.TokenId);

            modelBuilder.Entity<Transaction>().HasIndex(t => t.BlockTime);

            // WhaleActivity
            modelBuilder
                .Entity<WhaleActivity>()
                .HasOne(wa => wa.Wallet)
                .WithMany(w => w.WhaleActivities)
                .HasForeignKey(wa => wa.WalletId);

            modelBuilder
                .Entity<WhaleActivity>()
                .HasOne(wa => wa.Token)
                .WithMany(t => t.WhaleActivities)
                .HasForeignKey(wa => wa.TokenId);

            modelBuilder.Entity<WhaleActivity>().HasIndex(wa => new { wa.TokenId, wa.Timestamp });

            // Alert
            modelBuilder
                .Entity<Alert>()
                .HasOne(a => a.User)
                .WithMany(u => u.Alerts)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Alert>().HasIndex(a => new { a.TokenId, a.AlertType });

            // DumpEvent
            modelBuilder
                .Entity<DumpEvent>()
                .HasOne(de => de.Token)
                .WithMany(t => t.DumpEvents)
                .HasForeignKey(de => de.TokenId);

            // WalletMetric
            modelBuilder.Entity<WalletMetric>().HasKey(wm => wm.WalletId);

            modelBuilder
                .Entity<WalletMetric>()
                .HasOne(wm => wm.Wallet)
                .WithOne(w => w.WalletMetric)
                .HasForeignKey<WalletMetric>(wm => wm.WalletId);

            // TokenMetric
            modelBuilder.Entity<TokenMetric>().HasKey(tm => tm.TokenId);

            modelBuilder
                .Entity<TokenMetric>()
                .HasOne(tm => tm.Token)
                .WithOne(t => t.TokenMetric)
                .HasForeignKey<TokenMetric>(tm => tm.TokenId);

            // TrendingToken
            modelBuilder.Entity<TrendingToken>().HasKey(tt => tt.TokenId);

            modelBuilder
                .Entity<TrendingToken>()
                .HasOne(tt => tt.Token)
                .WithOne(t => t.TrendingToken)
                .HasForeignKey<TrendingToken>(tt => tt.TokenId);

            // TopTrader
            modelBuilder
                .Entity<TopTrader>()
                .HasOne(tt => tt.Wallet)
                .WithMany(w => w.TopTraders)
                .HasForeignKey(tt => tt.WalletId);

            modelBuilder
                .Entity<TopTrader>()
                .HasOne(tt => tt.Token)
                .WithMany(t => t.TopTraders)
                .HasForeignKey(tt => tt.TokenId);
        }
    }
}
