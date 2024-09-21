using System;

namespace WalletScanner.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TxHash { get; set; }
        public int WalletId { get; set; }
        public int TokenId { get; set; } // Add this property
        public long? BlockNumber { get; set; }
        public DateTime? BlockTime { get; set; } // Add this property
        public bool? Status { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ValueUsd { get; set; }
        public string TransactionType { get; set; }
        public decimal? Fee { get; set; }

        // Navigation properties
        public Wallet Wallet { get; set; } // Ensure this is of type Wallet
        public Token Token { get; set; } // Add this navigation property
    }
}
