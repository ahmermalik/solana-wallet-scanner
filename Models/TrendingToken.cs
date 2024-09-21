using System;

namespace WalletScanner.Models
{
    public class TrendingToken
    {
        public int TokenId { get; set; }
        public int? Rank { get; set; }
        public decimal? Volume24hUSD { get; set; }
        public decimal? Price { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Token Token { get; set; }
    }
}
