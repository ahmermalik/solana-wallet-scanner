using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WalletScanner.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace WalletScanner.Services
{
    public class BirdseyeApiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BirdseyeApiService> _logger;

        public BirdseyeApiService(IConfiguration configuration, ILogger<BirdseyeApiService> logger, IHttpClientFactory httpClientFactory)
        {
            _apiKey = configuration["Birdeye:ApiKey"] 
                       ?? throw new ArgumentNullException("Birdeye:ApiKey is not configured.");
            _logger = logger;

            _httpClient = httpClientFactory.CreateClient("BirdseyeApi");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);
        }

        /// <summary>
        /// Retrieves the list of monitored tokens.
        /// </summary>
        /// <returns>List of token symbols.</returns>
        public async Task<List<string>> GetMonitoredTokensAsync()
        {
            // Assuming there's an endpoint to get all tokens. If not, use a predefined list.
            // Replace the URL with the actual endpoint.
            string endpoint = "https://public-api.birdeye.so/v1/token/all-market-list";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(content);
                    // Assuming the tokens are under "data.tokens"
                    var tokens = json["data"]?["tokens"]?.ToObject<List<string>>() ?? new List<string>();
                    return tokens;
                }
                else
                {
                    _logger.LogError($"Failed to fetch monitored tokens. Status Code: {response.StatusCode}");
                    return new List<string> { "SOL", "USDC", "XYZ" }; // Fallback to default tokens
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching monitored tokens.");
                return new List<string> { "SOL", "USDC", "XYZ" }; // Fallback to default tokens
            }
        }

        /// <summary>
        /// Retrieves the transaction history for a specific wallet and token.
        /// </summary>
        /// <param name="walletAddress">The wallet address to query.</param>
        /// <param name="token">The token symbol.</param>
        /// <returns>List of transactions.</returns>
        public async Task<List<TransactionViewModel>> GetTransactionHistoryAsync(string walletAddress, string token)
        {
            // Replace with the actual endpoint and query parameters as per Birdeye API documentation.
            string endpoint = "https://public-api.birdeye.so/v1/wallet/transaction-history-multichain";
            var requestUri = $"{endpoint}?wallet={walletAddress}&token={token}&chain=solana"; // Assuming Solana; adjust as needed

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(content);
                    var transactions = json["data"]?.ToObject<List<TransactionViewModel>>() ?? new List<TransactionViewModel>();
                    return transactions;
                }
                else
                {
                    _logger.LogError($"Failed to fetch transaction history for wallet: {walletAddress}, Token: {token}. Status Code: {response.StatusCode}");
                    return new List<TransactionViewModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while fetching transaction history for wallet: {walletAddress}, Token: {token}.");
                return new List<TransactionViewModel>();
            }
        }

        /// <summary>
        /// Retrieves the list of tokens for multiple wallets.
        /// </summary>
        /// <param name="walletAddresses">List of wallet addresses.</param>
        /// <returns>A dictionary mapping each wallet address to its list of tokens.</returns>
        public async Task<Dictionary<string, List<string>>> GetTokenListForWalletsAsync(List<string> walletAddresses)
        {
            var walletTokenMap = new Dictionary<string, List<string>>();

            foreach (var walletAddress in walletAddresses)
            {
                try
                {
                    // Assuming there's an endpoint to get tokens for a single wallet
                    // Replace with the actual endpoint as per Birdeye API documentation
                    string endpoint = $"https://public-api.birdeye.so/v1/wallet/{walletAddress}/tokens";

                    HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(content);
                        // Assuming the tokens are under "data.tokens"
                        var tokens = json["data"]?["tokens"]?.ToObject<List<string>>() ?? new List<string>();
                        walletTokenMap.Add(walletAddress, tokens);
                    }
                    else
                    {
                        _logger.LogError($"Failed to fetch tokens for wallet: {walletAddress}. Status Code: {response.StatusCode}");
                        walletTokenMap.Add(walletAddress, new List<string>()); // Empty list as fallback
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Exception occurred while fetching tokens for wallet: {walletAddress}.");
                    walletTokenMap.Add(walletAddress, new List<string>()); // Empty list as fallback
                }
            }

            return walletTokenMap;
        }
    }
}
