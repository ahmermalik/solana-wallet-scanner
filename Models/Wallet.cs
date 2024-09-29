using System;
using System.Collections.Generic;

namespace WalletScanner.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int NetworkId { get; set; }  // Added NetworkId
        public string Address { get; set; }
        public decimal? TotalUsdValue { get; set; }
        public DateTime? LastUpdated { get; set; }

        // Navigation property for Network
        public Network Network { get; set; }  // Added Network navigation property

        // Navigation properties
        public ICollection<WalletHolding> WalletHoldings { get; set; }

        // Updated properties
        public ICollection<Transaction> IncomingTransactions { get; set; }  // Changed from Transactions
        public ICollection<Transaction> OutgoingTransactions { get; set; }  // Added OutgoingTransactions
        public ICollection<WhaleActivity> WhaleActivities { get; set; }
        public WalletMetric WalletMetric { get; set; }
        public ICollection<TopTrader> TopTraders { get; set; }
        public ICollection<Alert> Alerts { get; set; }  // Added Alerts
    }
}
