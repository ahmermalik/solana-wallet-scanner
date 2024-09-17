namespace WalletScanner.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public decimal ThresholdAmount { get; set; }
        public string AlertType { get; set; } // e.g., "Dump", "Accumulation"
        public DateTime CreatedAt { get; set; }
        // Additional fields like NotificationSettings can be added
    }
}
