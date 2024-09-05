namespace WalletScanner.ViewModels
{
    public class WalletViewModel
    {
        public string WalletAddress { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // buy or sell
        public DateTime Timestamp { get; set; }
    }
}

