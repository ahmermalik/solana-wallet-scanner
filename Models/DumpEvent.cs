using System;

namespace WalletScanner.Models
{
    public class DumpEvent
    {
        public int DumpEventId { get; set; } // Primary Key

        // Token relationship
        public int TokenId { get; set; }
        public Token Token { get; set; }

        // Event Details
        public decimal VolumeSold { get; set; }
        public decimal PriceDropPercent { get; set; }
        public DateTime DetectedAt { get; set; }
        public string Details { get; set; }

        // Optional: If you need to track the wallet involved
        public int? WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
