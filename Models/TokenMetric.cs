using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletScanner.Models
{
    public class TokenMetric
    {
        [Key]
        [ForeignKey("Token")]
        public int TokenId { get; set; } // Primary Key and Foreign Key to Token

        public int NetworkId { get; set; }   // Added NetworkId

        public Network Network { get; set; } // Added Network navigation property

        public string TopPerformingWallets { get; set; } // Could be JSON or a complex type

        public string CorrelationData { get; set; }      // Could be JSON or a complex type

        public decimal? TotalUsdValue { get; set; }      // Added property

        public DateTime? LastUpdated { get; set; }

        // Navigation properties
        public Token Token { get; set; }
    }
}
