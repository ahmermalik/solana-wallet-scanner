namespace WalletScanner.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
