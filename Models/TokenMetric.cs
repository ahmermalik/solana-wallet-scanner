using System;

namespace WalletScanner.Models
{
    public class TokenMetric
    {
        public int TokenId { get; set; }
        public string TopPerformingWallets { get; set; } // You can use a complex type or JSON
        public string CorrelationData { get; set; } // You can use a complex type or JSON
        public DateTime? LastUpdated { get; set; }

        // Navigation properties
        public Token Token { get; set; }
    }
}
