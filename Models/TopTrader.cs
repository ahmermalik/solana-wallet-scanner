using System;

namespace WalletScanner.Models
{
    public class TopTrader
    {
        public int Id { get; set; }
        public int TokenId { get; set; }
        public int WalletId { get; set; }
        public decimal? Volume { get; set; }
        public int? TradeCount { get; set; }
        public int? TradeBuyCount { get; set; }
        public int? TradeSellCount { get; set; }
        public decimal? VolumeBuy { get; set; }
        public decimal? VolumeSell { get; set; }
        public string Period { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Token Token { get; set; }
        public Wallet Wallet { get; set; }
    }
}
