using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WalletScanner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize BirdseyeApiService with your API key
            var service = new Services.BirdseyeApiService("8dd3bdb050ee480ba16b3c3e15a35db2");

            // Define the array of wallet addresses
            var wallets = new List<string>
            {
                "CTFJEcxBjbx8yP8siAqiyQ9QSg7bS3kPH43oRobjsWXw",
                "55NQkFDwwW8noThkL9Rd5ngbgUU36fYZeos1k5ZwjGdn",
                "Hw1T93eztqiqpPD8g3n5nHLTYxvBzNbx1SkpT9GJ65KW",
                "9XA8NLjsubEMmX5nLXcX9RyHDVyGdzWAAiPugQyJb2wg",
                "5iC1yoXYmUGsGBBLSKTgedya4cjQokaD3DxYoUTozz3c",
                "j1oeQoPeuEDmjvyMwBmCWexzCQup77kbKKxV59CnYbd",
                // Add as many wallets as needed
            };

            // Loop through each wallet address and fetch token data
            foreach (var wallet in wallets)
            {
                try
                {
                    var response = await service.GetTokenListForWalletAsync(wallet);
                    Console.WriteLine($"Wallet: {wallet}, Response: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data for wallet {wallet}: {ex.Message}");
                }
            }
        }
    }
}
