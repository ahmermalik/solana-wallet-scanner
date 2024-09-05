namespace WalletScanner.Models
{
    public class Metrics
    {
        public string WalletAddress { get; set; }
        public decimal Profitability { get; set; }
        public decimal WinLossRatio { get; set; }
        public TimeSpan AverageHoldTime { get; set; }
    }
}
