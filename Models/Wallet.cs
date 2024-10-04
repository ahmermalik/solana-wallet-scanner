using System;
using System.Collections.Generic;

namespace WalletScanner.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int NetworkId { get; set; }
        public string Address { get; set; }
        public decimal? TotalUsdValue { get; set; }
        public DateTime? LastUpdated { get; set; }

        public string? Name { get; set; }

        public Network Network { get; set; }

        public ICollection<WalletHolding> WalletHoldings { get; set; }

        // Updated properties
        public ICollection<Transaction> IncomingTransactions { get; set; }
        public ICollection<Transaction> OutgoingTransactions { get; set; }
        public ICollection<WhaleActivity> WhaleActivities { get; set; }
        public WalletMetric WalletMetric { get; set; }
        public ICollection<TopTrader> TopTraders { get; set; }
        public ICollection<Alert> Alerts { get; set; }
    }
}
