namespace WalletScanner.ViewModels
{
    public class AlertViewModel
    {
        public string Token { get; set; }
        public decimal ThresholdAmount { get; set; }
        public string AlertType { get; set; } // e.g., "Dump", "Accumulation"
        // Additional fields like NotificationPreferences can be added
    }
}
