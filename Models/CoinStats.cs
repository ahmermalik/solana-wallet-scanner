namespace WalletScanner.Models
{
    public class CoinStats
    {
        public string Token { get; set; }
        public decimal Accumulation { get; set; }
        public decimal Distribution { get; set; }
        public decimal WhaleActivity { get; set; }
    }
}
