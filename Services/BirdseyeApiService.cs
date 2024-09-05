using RestSharp;
using System.Threading.Tasks;
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
            var options = new RestClientOptions($"https://public-api.birdeye.so/v1/wallet/multichain_token_list?wallet={walletAddress}");
            var client = new RestClient(options);
            var request = new RestRequest();

            var response = await client.GetAsync(request);
            return response.Content;
        }

        // Add this method to handle multiple wallets
        public async Task<List<string>> GetTokenListForWalletsAsync(List<string> walletAddresses)
        {
            var results = new List<string>();
            foreach (var walletAddress in walletAddresses)
            {
                var result = await GetTokenListForWalletAsync(walletAddress);
                results.Add(result);
            }
            return results;
        }
    }
}
