namespace WalletScanner.Helpers
{
    public static class DateTimeHelper
    {
        public static string UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Convert Unix time (seconds since 1970) to DateTime
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);

            // Format the DateTime into a readable string: yyyy-MM-dd HH:mm:ss
            return dateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
