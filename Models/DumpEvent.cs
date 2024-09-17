namespace WalletScanner.Models
{
    public class DumpEvent
    {
        public string WalletAddress { get; set; }
        public decimal TotalSold { get; set; }
        public int SellCount { get; set; }
        public DateTime LastSellTime { get; set; }
    }
}
