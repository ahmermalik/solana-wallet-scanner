namespace WalletScanner.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
        public Wallet Wallet { get; set; }
    }
}
