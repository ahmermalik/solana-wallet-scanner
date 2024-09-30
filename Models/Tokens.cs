using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WalletScanner.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public int NetworkId { get; set; }
        public string? Address { get; set; }
        public Network Network { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int? Decimals { get; set; }
        public decimal? Liquidity { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? Price { get; set; }
        public decimal? Volume24hUSD { get; set; }
        public decimal? PriceChangePercent24h { get; set; }
        public string? LogoURI { get; set; }
        public DateTime? LastUpdated { get; set; }

        // Navigation properties
        public ICollection<WalletHolding> WalletHoldings { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<WhaleActivity> WhaleActivities { get; set; }
        public ICollection<DumpEvent> DumpEvents { get; set; }
        public ICollection<Alert> Alerts { get; set; }
        public ICollection<TopTrader> TopTraders { get; set; }
        public TokenMetric TokenMetric { get; set; }
        public TrendingToken TrendingToken { get; set; }
    }
}
