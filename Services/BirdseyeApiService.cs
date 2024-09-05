using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace WalletScanner.Services
{
    public class BirdseyeApiService
    {
        private readonly string _apiKey;

        public BirdseyeApiService(string apiKey)
        {
            _apiKey = apiKey;
        }

        // Existing method for a single wallet
        public async Task<string> GetTokenListForWalletAsync(string walletAddress)
        {
            var options = new RestClientOptions(
                $"https://public-api.birdeye.so/v1/wallet/multichain_token_list?wallet={walletAddress}"
            );
            var client = new RestClient(options);
            var request = new RestRequest();

            var response = await client.GetAsync(request);

            // Check if the response is null or has no content
            if (response == null || response.Content == null)
            {
                Console.WriteLine(
                    $"No response from API or content is null for wallet: {walletAddress}"
                );
                return null;
            }

            Console.WriteLine($"API Response for {walletAddress}: {response.Content}");
            return response.Content;
        }

        // Method to handle multiple wallets
        public async Task<List<string>> GetTokenListForWalletsAsync(List<string> walletAddresses)
        {
            var results = new List<string>();
            foreach (var walletAddress in walletAddresses)
            {
                var result = await GetTokenListForWalletAsync(walletAddress);

                // Only add the result if it's not null
                if (result != null)
                {
                    results.Add(result);
                }
                else
                {
                    Console.WriteLine($"Skipping null result for wallet: {walletAddress}");
                }
            }
            return results;
        }
    }
}
