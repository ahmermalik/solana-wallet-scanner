using System;

namespace WalletScanner.Models
{
    public class Alert
    {
        public int AlertId { get; set; } // Changed Id to AlertId for consistency

        // User relationship
        public int UserId { get; set; }
        public User User { get; set; }

        // Token relationship
        public int? TokenId { get; set; }
        public Token Token { get; set; }

        // Wallet relationship
        public int? WalletId { get; set; }   // Added WalletId
        public Wallet Wallet { get; set; }   // Added Wallet navigation property

        // Network relationship
        public int? NetworkId { get; set; }  // Added NetworkId
        public Network Network { get; set; } // Added Network navigation property

        // Alert details
        public string AlertType { get; set; } // e.g., "Dump", "Accumulation"
        public string Message { get; set; }   // Added Message property
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Removed ThresholdAmount (if no longer needed)
    }
}
