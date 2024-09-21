using System.Collections.Generic;

namespace WalletScanner.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public string Address { get; set; }
        public decimal? TotalUsdValue { get; set; }
        public DateTime? LastUpdated { get; set; }

        // Add this property
        public ICollection<WalletHolding> WalletHoldings { get; set; }

        // Existing properties
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<WhaleActivity> WhaleActivities { get; set; }
        public WalletMetric WalletMetric { get; set; }
        public ICollection<TopTrader> TopTraders { get; set; }
    }
}
