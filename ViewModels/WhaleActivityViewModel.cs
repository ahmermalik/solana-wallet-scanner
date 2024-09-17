namespace WalletScanner.ViewModels
{
    public class WhaleActivityViewModel
    {
        public required string Token { get; set; }
        public required string WalletAddress { get; set; }
        public decimal Amount { get; set; }
        public required string ActivityType { get; set; } // e.g., "Dump", "Accumulation"
        public DateTime Timestamp { get; set; }
    }
}
