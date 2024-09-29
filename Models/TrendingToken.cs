using System;

namespace WalletScanner.Models
{
    public class TrendingToken
    {
        public int TokenId { get; set; }
        public int NetworkId { get; set; }    // Added NetworkId
        public Network Network { get; set; }  // Added Network navigation property
        public int? Rank { get; set; }
        public decimal? Volume24hUSD { get; set; }
        public decimal? Price { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Token Token { get; set; }
    }
}
