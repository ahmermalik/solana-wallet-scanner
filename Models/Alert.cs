namespace WalletScanner.Models
{
    public class Alert
    {
        public int Id { get; set; } // Primary Key

        // User relationship
        public int UserId { get; set; }
        public User User { get; set; }

        // Token relationship
        public int? TokenId { get; set; } // Nullable if alerts can exist without a token
        public Token Token { get; set; }

        // Alert details
        public decimal ThresholdAmount { get; set; }
        public string AlertType { get; set; } // e.g., "Dump", "Accumulation"
        public DateTime CreatedAt { get; set; }
    }
}
