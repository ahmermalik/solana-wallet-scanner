using System;
using Newtonsoft.Json;

namespace WalletScanner.ViewModels
{
    public class TransactionViewModel
    {
        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; } // e.g., "buy", "sell"

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        // Add other properties as per the Birdeye API response

        // Example:
        // [JsonProperty("from_address")]
        // public string FromAddress { get; set; }

        // [JsonProperty("to_address")]
        // public string ToAddress { get; set; }
    }
}
