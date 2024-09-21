using System;

namespace WalletScanner.Models
{
    public class WalletHolding
    {
        public int WalletId { get; set; }
        public int TokenId { get; set; }
        public decimal? Balance { get; set; }
        public decimal? UiAmount { get; set; }
        public decimal? ValueUsd { get; set; }
        public decimal? PriceUsd { get; set; }

        // Navigation properties
        public Wallet Wallet { get; set; }
        public Token Token { get; set; }
    }
}
