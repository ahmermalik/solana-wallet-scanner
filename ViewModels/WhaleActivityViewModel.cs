namespace WalletScanner.ViewModels
{
    public class WhaleActivityViewModel
    {
        public string Token { get; set; }
        public string WalletAddress { get; set; }
        public decimal Amount { get; set; }
        public string ActivityType { get; set; } // "Accumulation" or "Distribution"
        public DateTime Timestamp { get; set; }
    }
}
