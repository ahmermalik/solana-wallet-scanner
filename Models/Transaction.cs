using System;
using System.Collections.Generic;

namespace WalletScanner.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TxHash { get; set; }
        public int? FromWalletId { get; set; }  // Replaced WalletId with FromWalletId
        public Wallet FromWallet { get; set; }  // Added FromWallet navigation property
        public int? ToWalletId { get; set; }    // Added ToWalletId
        public Wallet ToWallet { get; set; }    // Added ToWallet navigation property
        public int TokenId { get; set; }
        public Token Token { get; set; }
        public int NetworkId { get; set; }      // Added NetworkId
        public Network Network { get; set; }    // Added Network navigation property
        public long? BlockNumber { get; set; }
        public DateTime? BlockTime { get; set; }
        public bool? Status { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ValueUsd { get; set; }
        public string TransactionType { get; set; }
        public decimal? Fee { get; set; }

        // Removed WalletId and Wallet properties
    }
}
