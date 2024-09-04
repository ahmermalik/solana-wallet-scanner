using RestSharp;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BirdseyeApiService
{
    private readonly string _apiKey;

    public BirdseyeApiService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GetTokenListForWalletAsync(string walletAddress)
    {
        var options = new RestClientOptions($"https://public-api.birdeye.so/v1/wallet/multichain_token_list?wallet={walletAddress}");
        var client = new RestClient(options);
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        return response.Content;
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // Initialize BirdseyeApiService with your API key
        var service = new BirdseyeApiService("YOUR_API_KEY");

        // Define the array of wallet addresses
        var wallets = new List<string>
        {
            "0xf584f8728b874a6a5c7a8d4d387c9aae9172d621",
            "wallet2",
            "wallet3",
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
