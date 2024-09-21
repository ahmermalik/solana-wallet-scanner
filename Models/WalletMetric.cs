using System;

namespace WalletScanner.Models
{
    public class WalletMetric
    {
        public int WalletId { get; set; }
        public decimal? Profitability { get; set; }
        public decimal? WinLossRatio { get; set; }
        public decimal? AverageHoldTime { get; set; }
        public decimal? CostBasis { get; set; }
        public int? TradeFrequency { get; set; }
        public decimal? TradeSizeAverage { get; set; }
        public DateTime? LastUpdated { get; set; }

        // Navigation properties
        public Wallet Wallet { get; set; }
    }
}
