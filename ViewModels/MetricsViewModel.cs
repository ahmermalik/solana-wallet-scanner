namespace WalletScanner.ViewModels
{
    public class MetricsViewModel
    {
        public string WalletAddress { get; set; }
        public decimal Profitability { get; set; }
        public decimal WinLossRatio { get; set; }
        public decimal AverageHoldTime { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
