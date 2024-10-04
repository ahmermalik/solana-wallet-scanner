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

            // ===============================
            // Networks Configuration
            // ===============================
            modelBuilder.Entity<Network>().HasKey(n => n.NetworkId);

            modelBuilder.Entity<Network>().HasIndex(n => n.Name).IsUnique();

            // ===============================
            // Wallets Configuration
            // ===============================
            modelBuilder
                .Entity<Wallet>()
                .HasIndex(w => new { w.NetworkId, w.Address })
                .IsUnique();

            modelBuilder
                .Entity<Wallet>()
                .HasOne(w => w.Network)
                .WithMany(n => n.Wallets)
                .HasForeignKey(w => w.NetworkId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // Tokens Configuration
            // ===============================
            modelBuilder
                .Entity<Token>()
                .HasIndex(t => new { t.NetworkId, t.Address })
                .IsUnique();

            modelBuilder
                .Entity<Token>()
                .HasOne(t => t.Network)
                .WithMany(n => n.Tokens)
                .HasForeignKey(t => t.NetworkId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Token>(entity =>
            {
                // Configure decimal properties with increased precision and scale
                entity.Property(e => e.Liquidity).HasPrecision(20, 6);

                entity.Property(e => e.MarketCap).HasPrecision(20, 6);

                entity.Property(e => e.Price).HasPrecision(20, 6);

                entity.Property(e => e.Volume24hUSD).HasPrecision(20, 6);

                entity.Property(e => e.PriceChangePercent24h).HasPrecision(8, 6); // Increased scale for percentage precision

                // Configure string properties if necessary
                entity.Property(e => e.Symbol).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });

            // ===============================
            // WalletHoldings Configuration
            // ===============================
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

            modelBuilder.Entity<WalletHolding>(entity =>
            {
                // Configure decimal properties with increased precision and scale
                entity.Property(e => e.Balance).HasPrecision(25, 6);

                entity.Property(e => e.UiAmount).HasPrecision(25, 6);

                entity.Property(e => e.ValueUsd).HasPrecision(25, 6);

                entity.Property(e => e.PriceUsd).HasPrecision(25, 6);
            });

            // ===============================
            // Transactions Configuration
            // ===============================
            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.FromWallet)
                .WithMany(w => w.OutgoingTransactions)
                .HasForeignKey(t => t.FromWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.ToWallet)
                .WithMany(w => w.IncomingTransactions)
                .HasForeignKey(t => t.ToWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.Token)
                .WithMany(tk => tk.Transactions)
                .HasForeignKey(t => t.TokenId);

            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.Network)
                .WithMany(n => n.Transactions)
                .HasForeignKey(t => t.NetworkId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Amount).HasPrecision(25, 6);

                entity.Property(e => e.Fee).HasPrecision(25, 6);

                entity.Property(e => e.ValueUsd).HasPrecision(25, 6);
            });

            // ===============================
            // DumpEvent Configuration
            // ===============================
            modelBuilder.Entity<DumpEvent>(entity =>
            {
                entity.Property(e => e.PriceDropPercent).HasPrecision(8, 6); // Increased scale for percentage precision

                entity.Property(e => e.VolumeSold).HasPrecision(20, 6);
            });

            // ===============================
            // WalletMetric Configuration
            // ===============================
            modelBuilder.Entity<WalletMetric>(entity =>
            {
                entity.HasKey(wm => wm.WalletId);

                entity.Property(wm => wm.TotalUsdValue).HasPrecision(25, 6);

                entity.Property(wm => wm.Profitability).HasPrecision(25, 6);

                entity.Property(wm => wm.WinLossRatio).HasPrecision(25, 6);

                entity.Property(wm => wm.AverageHoldTime).HasPrecision(25, 6);

                entity.Property(wm => wm.CostBasis).HasPrecision(25, 6);

                entity.Property(wm => wm.TradeSizeAverage).HasPrecision(25, 6);
            });

            // ===============================
            // TokenMetric Configuration
            // ===============================
            modelBuilder.Entity<TokenMetric>(entity =>
            {
                entity.HasKey(tm => tm.TokenId); // Define TokenId as Primary Key

                entity
                    .HasOne(tm => tm.Token)
                    .WithOne(t => t.TokenMetric)
                    .HasForeignKey<TokenMetric>(tm => tm.TokenId)
                    .OnDelete(DeleteBehavior.Cascade); // Configure 1-1 relationship

                entity
                    .HasOne(tm => tm.Network)
                    .WithMany(n => n.TokenMetrics)
                    .HasForeignKey(tm => tm.NetworkId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure other properties as needed
                entity.Property(tm => tm.TopPerformingWallets).HasMaxLength(500); // Example: Adjust based on your requirements

                entity.Property(tm => tm.CorrelationData).HasMaxLength(1000); // Example: Adjust based on your requirements

                entity.Property(tm => tm.TotalUsdValue).HasPrecision(25, 6);

                entity.Property(tm => tm.LastUpdated).HasColumnType("datetime");
            });

            // ===============================
            // TrendingToken Configuration
            // ===============================
            modelBuilder.Entity<TrendingToken>(entity =>
            {
                entity.HasKey(tt => tt.TokenId); // Define TokenId as Primary Key

                entity
                    .HasOne(tt => tt.Token)
                    .WithOne(t => t.TrendingToken)
                    .HasForeignKey<TrendingToken>(tt => tt.TokenId)
                    .OnDelete(DeleteBehavior.Cascade); // Configure 1-1 relationship

                entity
                    .HasOne(tt => tt.Network)
                    .WithMany(n => n.TrendingTokens)
                    .HasForeignKey(tt => tt.NetworkId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure other properties as needed
                entity.Property(tt => tt.Volume24hUSD).HasPrecision(25, 6);

                entity.Property(tt => tt.Price).HasPrecision(25, 6);

                entity.Property(tt => tt.UpdatedAt).HasColumnType("datetime");
            });

            // ===============================
            // TopTrader Configuration
            // ===============================
            modelBuilder.Entity<TopTrader>(entity =>
            {
                entity.Property(tt => tt.Volume).HasPrecision(25, 6);

                entity.Property(tt => tt.VolumeBuy).HasPrecision(25, 6);

                entity.Property(tt => tt.VolumeSell).HasPrecision(25, 6);
            });

            // ===============================
            // WhaleActivity Configuration
            // ===============================
            modelBuilder.Entity<WhaleActivity>(entity =>
            {
                entity.HasKey(wa => wa.WhaleActivityId);

                entity
                    .HasOne(wa => wa.Wallet)
                    .WithMany(w => w.WhaleActivities)
                    .HasForeignKey(wa => wa.WalletId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(wa => wa.Token)
                    .WithMany(t => t.WhaleActivities)
                    .HasForeignKey(wa => wa.TokenId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(wa => wa.Network)
                    .WithMany(n => n.WhaleActivities)
                    .HasForeignKey(wa => wa.NetworkId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(wa => wa.Amount).HasPrecision(20, 6);

                entity.Property(wa => wa.ValueUsd).HasPrecision(20, 6);

                entity.Property(wa => wa.Timestamp).HasColumnType("datetime");
            });

            // ===============================
            // Alert Configuration
            // ===============================
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(a => a.AlertId);

                entity
                    .HasOne(a => a.User)
                    .WithMany(u => u.Alerts)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(a => a.Token)
                    .WithMany(t => t.Alerts)
                    .HasForeignKey(a => a.TokenId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(a => a.Wallet)
                    .WithMany(w => w.Alerts)
                    .HasForeignKey(a => a.WalletId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(a => a.Network)
                    .WithMany(n => n.Alerts)
                    .HasForeignKey(a => a.NetworkId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure other properties as needed
                entity.Property(a => a.AlertType).IsRequired().HasMaxLength(50);

                entity.Property(a => a.Message).IsRequired().HasMaxLength(500);

                entity.Property(a => a.CreatedAt).HasColumnType("datetime");
            });

            // ===============================
            // Continue configuring other entities similarly...
            // ===============================
        }
    }
}
