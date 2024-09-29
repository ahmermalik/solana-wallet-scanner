using Microsoft.EntityFrameworkCore;
using WalletScanner.Models;

namespace WalletScanner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Network> Networks { get; set; }
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

            // Networks
            modelBuilder.Entity<Network>()
                .HasKey(n => n.NetworkId);

            modelBuilder.Entity<Network>()
                .HasIndex(n => n.Name)
                .IsUnique();

            // Wallets
            modelBuilder.Entity<Wallet>()
                .HasIndex(w => new { w.NetworkId, w.Address })
                .IsUnique();

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.Network)
                .WithMany(n => n.Wallets)
                .HasForeignKey(w => w.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            // Tokens
            modelBuilder.Entity<Token>()
                .HasIndex(t => new { t.NetworkId, t.Address })
                .IsUnique();

            modelBuilder.Entity<Token>()
                .HasOne(t => t.Network)
                .WithMany(n => n.Tokens)
                .HasForeignKey(t => t.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            // WalletHoldings
            modelBuilder.Entity<WalletHolding>()
                .HasKey(wh => new { wh.WalletId, wh.TokenId });

            modelBuilder.Entity<WalletHolding>()
                .HasOne(wh => wh.Wallet)
                .WithMany(w => w.WalletHoldings)
                .HasForeignKey(wh => wh.WalletId);

            modelBuilder.Entity<WalletHolding>()
                .HasOne(wh => wh.Token)
                .WithMany(t => t.WalletHoldings)
                .HasForeignKey(wh => wh.TokenId);

            // Transactions
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromWallet)
                .WithMany(w => w.OutgoingTransactions)
                .HasForeignKey(t => t.FromWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToWallet)
                .WithMany(w => w.IncomingTransactions)
                .HasForeignKey(t => t.ToWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Token)
                .WithMany(tk => tk.Transactions)
                .HasForeignKey(t => t.TokenId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Network)
                .WithMany(n => n.Transactions)
                .HasForeignKey(t => t.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.FromWalletId);

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.ToWalletId);

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.TokenId);

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.BlockTime);

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.NetworkId);

            // WhaleActivity
            modelBuilder.Entity<WhaleActivity>()
                .HasOne(wa => wa.Wallet)
                .WithMany(w => w.WhaleActivities)
                .HasForeignKey(wa => wa.WalletId);

            modelBuilder.Entity<WhaleActivity>()
                .HasOne(wa => wa.Token)
                .WithMany(t => t.WhaleActivities)
                .HasForeignKey(wa => wa.TokenId);

            modelBuilder.Entity<WhaleActivity>()
                .HasOne(wa => wa.Network)
                .WithMany(n => n.WhaleActivities)
                .HasForeignKey(wa => wa.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            modelBuilder.Entity<WhaleActivity>()
                .HasIndex(wa => new { wa.TokenId, wa.Timestamp });

            // Alert
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.User)
                .WithMany(u => u.Alerts)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Token)
                .WithMany(t => t.Alerts)
                .HasForeignKey(a => a.TokenId);

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Wallet)
                .WithMany(w => w.Alerts)
                .HasForeignKey(a => a.WalletId);

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Network)
                .WithMany(n => n.Alerts)
                .HasForeignKey(a => a.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            modelBuilder.Entity<Alert>()
                .HasIndex(a => new { a.TokenId, a.AlertType });

            // DumpEvent
            modelBuilder.Entity<DumpEvent>()
                .HasOne(de => de.Token)
                .WithMany(t => t.DumpEvents)
                .HasForeignKey(de => de.TokenId);

            modelBuilder.Entity<DumpEvent>()
                .HasOne(de => de.Network)
                .WithMany(n => n.DumpEvents)
                .HasForeignKey(de => de.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            // WalletMetric
            modelBuilder.Entity<WalletMetric>()
                .HasKey(wm => wm.WalletId);

            modelBuilder.Entity<WalletMetric>()
                .HasOne(wm => wm.Wallet)
                .WithOne(w => w.WalletMetric)
                .HasForeignKey<WalletMetric>(wm => wm.WalletId);

            // TokenMetric
            modelBuilder.Entity<TokenMetric>()
                .HasKey(tm => tm.TokenId);

            modelBuilder.Entity<TokenMetric>()
                .HasOne(tm => tm.Token)
                .WithOne(t => t.TokenMetric)
                .HasForeignKey<TokenMetric>(tm => tm.TokenId);

            modelBuilder.Entity<TokenMetric>()
                .HasOne(tm => tm.Network)
                .WithMany(n => n.TokenMetrics)
                .HasForeignKey(tm => tm.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            // TrendingToken
            modelBuilder.Entity<TrendingToken>()
                .HasKey(tt => tt.TokenId);

            modelBuilder.Entity<TrendingToken>()
                .HasOne(tt => tt.Token)
                .WithOne(t => t.TrendingToken)
                .HasForeignKey<TrendingToken>(tt => tt.TokenId);

            modelBuilder.Entity<TrendingToken>()
                .HasOne(tt => tt.Network)
                .WithMany(n => n.TrendingTokens)
                .HasForeignKey(tt => tt.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            modelBuilder.Entity<TrendingToken>()
                .HasIndex(tt => tt.Rank);

            // TopTrader
            modelBuilder.Entity<TopTrader>()
                .HasOne(tt => tt.Wallet)
                .WithMany(w => w.TopTraders)
                .HasForeignKey(tt => tt.WalletId);

            modelBuilder.Entity<TopTrader>()
                .HasOne(tt => tt.Token)
                .WithMany(t => t.TopTraders)
                .HasForeignKey(tt => tt.TokenId);

            modelBuilder.Entity<TopTrader>()
                .HasOne(tt => tt.Network)
                .WithMany(n => n.TopTraders)
                .HasForeignKey(tt => tt.NetworkId)
                .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

            modelBuilder.Entity<TopTrader>()
                .HasIndex(tt => tt.TokenId);

            modelBuilder.Entity<TopTrader>()
                .HasIndex(tt => tt.WalletId);
        }
    }
}
