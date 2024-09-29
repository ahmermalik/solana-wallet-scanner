using System;

namespace WalletScanner.Models
{
    public class WhaleActivity
    {
        public int WhaleActivityId { get; set; } // Primary Key

        // Foreign Keys and Navigation Properties
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int TokenId { get; set; }
        public Token Token { get; set; }
        public int NetworkId { get; set; }      // Added NetworkId
        public Network Network { get; set; }    // Added Network navigation property

        // Activity Details
        public string ActivityType { get; set; } // e.g., "Buy" or "Sell"
        public decimal Amount { get; set; }
        public decimal ValueUsd { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
