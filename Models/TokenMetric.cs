using System;

namespace WalletScanner.Models
{
    public class TokenMetric
    {
        public int TokenId { get; set; }
        public int NetworkId { get; set; }   // Added NetworkId
        public Network Network { get; set; } // Added Network navigation property
        public string TopPerformingWallets { get; set; } // Could be JSON or a complex type
        public string CorrelationData { get; set; }      // Could be JSON or a complex type
        public DateTime? LastUpdated { get; set; }

        // Navigation properties
        public Token Token { get; set; }
    }
}
