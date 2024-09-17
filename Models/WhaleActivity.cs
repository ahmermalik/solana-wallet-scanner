namespace WalletScanner.Models
{
    public class WhaleActivity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string WalletAddress { get; set; }
        public decimal Amount { get; set; }
        public string ActivityType { get; set; } // e.g., "Dump", "Accumulation"
        public DateTime Timestamp { get; set; }
    }
}
