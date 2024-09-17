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
/*
CoinStats.cs
Description: Represents statistics related to a specific coin/token.
Properties:
Token
Accumulation
Distribution
WhaleActivity
*/
