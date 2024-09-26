using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; // Newtonsoft.Json for JSON formatting
using Newtonsoft.Json.Linq;
using WalletScanner.Helpers;

namespace WalletScanner.Services
{
    public class BirdseyeApiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BirdseyeApiService> _logger;

        public BirdseyeApiService(
            IConfiguration configuration,
            ILogger<BirdseyeApiService> logger,
            IHttpClientFactory httpClientFactory
        )
        {
            _apiKey =
                configuration["Birdeye:ApiKey"]
                ?? throw new ArgumentNullException("Birdeye:ApiKey is not configured.");
            _logger = logger;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);
        }

        /// <summary>
        /// Retrieves the list of monitored tokens.
        /// </summary>
        /// <returns>List of token symbols.</returns>
        // public async Task<List<string>> GetMonitoredTokensAsync()
        // {
        //     string endpoint = "https://public-api.birdeye.so/defi/price?address=So11111111111111111111111111111111111111112";

        //     try
        //     {
        //         HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        //         if (response.IsSuccessStatusCode)
        //         {
        //             string content = await response.Content.ReadAsStringAsync();
        //             JObject json = JObject.Parse(content);

        //             // Assuming tokens are under "data.tokens"
        //             var tokens = json["data"]?["tokens"]?.ToObject<List<string>>() ?? new List<string>();
        //             return tokens;
        //         }
        //         else
        //         {
        //             _logger.LogError($"Failed to fetch monitored tokens. Status Code: {response.StatusCode}");
        //             return new List<string> { "SOL", "USDC", "XYZ" }; // Fallback to default tokens
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Exception occurred while fetching monitored tokens.");
        //         return new List<string> { "SOL", "USDC", "XYZ" }; // Fallback to default tokens
        //     }
        // }

        /// <summary>
        /// Retrieves the transaction history for a specific wallet and token.
        /// </summary>
        /// <param name="walletAddress">The wallet address to query.</param>
        /// <param name="token">The token symbol.</param>
        /// <returns>List of transactions.</returns>
        // public async Task<List<string>> GetTransactionHistoryAsync(string walletAddress, string token)
        // {
        //     string endpoint = "https://public-api.birdeye.so/v1/wallet/tx_list";
        //     var requestUri = $"{endpoint}?wallet={walletAddress}&token={token}&chain=solana"; // Assuming Solana; adjust as needed

        //     try
        //     {
        //         HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        //         if (response.IsSuccessStatusCode)
        //         {
        //             string content = await response.Content.ReadAsStringAsync();
        //             JObject json = JObject.Parse(content);

        //             // Assuming the transactions are under "data.transactions"
        //             var transactions = json["data"]?["transactions"]?.ToObject<List<string>>() ?? new List<string>();
        //             return transactions;
        //         }
        //         else
        //         {
        //             _logger.LogError($"Failed to fetch transaction history for wallet: {walletAddress}, Token: {token}. Status Code: {response.StatusCode}");
        //             return new List<string>();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, $"Exception occurred while fetching transaction history for wallet: {walletAddress}, Token: {token}.");
        //         return new List<string>();
        //     }
        // }

        /// <summary>
        /// Retrieves the list of tokens for multiple wallets.
        /// </summary>
        /// <param name="walletAddresses">List of wallet addresses.</param>
        /// <returns>A dictionary mapping each wallet address to its list of tokens.</returns>
        public async Task<Dictionary<string, object>> GetTokenListForWalletsAsync(
            List<string> walletAddresses
        )
        {
            var walletDataMap = new Dictionary<string, object>();

            foreach (var walletAddress in walletAddresses)
            {
                try
                {
                    string endpoint =
                        $"https://public-api.birdeye.so/v1/wallet/token_list?wallet={walletAddress}";
                    HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                    var content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.Content.Headers.ContentType.MediaType == "application/json")
                        {
                            JObject json = JObject.Parse(content);

                            var data = json["data"];
                            if (data != null)
                            {
                                var items =
                                    data["items"]?.ToObject<List<JObject>>() ?? new List<JObject>();

                                var parsedItems = new List<object>();
                                foreach (var item in items)
                                {
                                    var parsedItem = new
                                    {
                                        address = item["address"]?.ToString(),
                                        decimals = item["decimals"]?.ToObject<int>(),
                                        balance = item["balance"]?.ToObject<long>(),
                                        uiAmount = item["uiAmount"]?.ToObject<decimal>(),
                                        chainId = item["chainId"]?.ToString(),
                                        name = item["name"]?.ToString(),
                                        symbol = item["symbol"]?.ToString(),
                                        logoURI = item["logoURI"]?.ToString(),
                                        priceUsd = item["priceUsd"]?.ToObject<decimal>(),
                                        valueUsd = item["valueUsd"]?.ToObject<decimal>(),
                                    };

                                    parsedItems.Add(parsedItem);
                                }

                                walletDataMap[walletAddress] = new
                                {
                                    wallet = data["wallet"]?.ToString(),
                                    totalUsd = data["totalUsd"]?.ToObject<decimal>(),
                                    items = parsedItems,
                                };
                            }
                            else
                            {
                                _logger.LogWarning(
                                    $"No 'data' field found in the response for wallet {walletAddress}"
                                );
                                walletDataMap[walletAddress] = new { error = "No data found" };
                            }
                        }
                        else
                        {
                            _logger.LogError(
                                $"Unexpected content type: {response.Content.Headers.ContentType.MediaType}"
                            );
                            walletDataMap[walletAddress] = new
                            {
                                error = "Unexpected content type",
                            };
                        }
                    }
                    else
                    {
                        _logger.LogError(
                            $"Failed to fetch data for wallet: {walletAddress}. Status Code: {response.StatusCode}"
                        );
                        walletDataMap[walletAddress] = new { error = "API call failed" };
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        $"Exception occurred while fetching data for wallet: {walletAddress}."
                    );
                    walletDataMap[walletAddress] = new { error = "Exception occurred" };
                }
            }

            string formattedResult = JsonConvert.SerializeObject(
                walletDataMap,
                Formatting.Indented
            );
            // _logger.LogInformation($"Results for wallets: {formattedResult}");

            return walletDataMap;
        }

        public async Task<Dictionary<string, object>> GetTrendingTokensAsync()
        {
            var trendingTokenDataMap = new Dictionary<string, object>();
            string endpoint = "https://public-api.birdeye.so/defi/token_trending";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

                // Adding the blockchain-specific header for Solana (if necessary)
                request.Headers.Add("x-chain", "solana");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(content);

                    var data = json["data"];
                    if (data != null)
                    {
                        var tokens =
                            data["tokens"]?.ToObject<List<JObject>>() ?? new List<JObject>();

                        var parsedTokens = new List<object>();
                        foreach (var token in tokens)
                        {
                            var parsedToken = new
                            {
                                address = token["address"]?.ToString(),
                                decimals = token["decimals"]?.ToObject<int>(),
                                liquidity = token["liquidity"]?.ToObject<decimal>(),
                                logoURI = token["logoURI"]?.ToString(),
                                name = token["name"]?.ToString(),
                                symbol = token["symbol"]?.ToString(),
                                volume24hUSD = token["volume24hUSD"]?.ToObject<decimal>(),
                                rank = token["rank"]?.ToObject<int>(),
                                price = token["price"]?.ToObject<decimal>(),
                            };

                            parsedTokens.Add(parsedToken);
                        }
                        trendingTokenDataMap["tokens"] = parsedTokens;
                        trendingTokenDataMap["updateTime"] =
                            WalletScanner.Helpers.DateTimeHelper.UnixTimeStampToDateTime(
                                data["updateTime"]?.ToObject<long>() ?? 0
                            );
                        trendingTokenDataMap["total"] = trendingTokenDataMap["total"] = data[
                            "total"
                        ]
                            ?.ToObject<int>();
                    }
                    else
                    {
                        _logger.LogWarning("No 'data' field found in the response.");
                        trendingTokenDataMap["error"] = "No data found";
                    }
                }
                else
                {
                    _logger.LogError(
                        $"Failed to fetch trending tokens. Status Code: {response.StatusCode}"
                    );
                    trendingTokenDataMap["error"] = "API call failed";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching trending tokens.");
                trendingTokenDataMap["error"] = "Exception occurred";
            }

            // Returning the parsed trending token data in the same format as the GetTokenListForWalletsAsync method
            return trendingTokenDataMap;
        }

        public async Task<Dictionary<string, object>> GetTokenListAsync()
        {
            var tokenDataMap = new Dictionary<string, object>();
            string endpoint =
                "https://public-api.birdeye.so/defi/tokenlist?sort_by=v24hUSD&sort_type=desc&offset=0&limit=50&min_liquidity=10000";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

                // Adding the blockchain-specific header for Solana
                request.Headers.Add("x-chain", "solana");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(content);

                    var data = json["data"];
                    if (data != null)
                    {
                        var tokens =
                            data["tokens"]?.ToObject<List<JObject>>() ?? new List<JObject>();

                        var parsedTokens = new List<object>();
                        foreach (var token in tokens)
                        {
                            var parsedToken = new
                            {
                                address = token["address"]?.ToString(),
                                decimals = token["decimals"]?.ToObject<int>(),
                                lastTradeUnixTime = WalletScanner.Helpers.DateTimeHelper.UnixTimeStampToDateTime(
                                    token["lastTradeUnixTime"]?.ToObject<long>() ?? 0
                                ),
                                liquidity = token["liquidity"]?.ToObject<decimal>(),
                                logoURI = token["logoURI"]?.ToString(),
                                mc = token["mc"]?.ToObject<decimal>(),
                                name = token["name"]?.ToString(),
                                symbol = token["symbol"]?.ToString(),
                                v24hChangePercent = token["v24hChangePercent"]
                                    ?.ToObject<decimal?>(),
                                v24hUSD = token["v24hUSD"]?.ToObject<decimal?>(),
                            };

                            parsedTokens.Add(parsedToken);
                        }

                        tokenDataMap["tokens"] = parsedTokens;
                        tokenDataMap["updateUnixTime"] = data["updateUnixTime"]?.ToObject<long>();
                        tokenDataMap["updateTime"] = data["updateTime"]?.ToString();
                        tokenDataMap["total"] = data["total"]?.ToObject<int>();
                    }
                    else
                    {
                        _logger.LogWarning("No 'data' field found in the response.");
                        tokenDataMap["error"] = "No data found";
                    }
                }
                else
                {
                    _logger.LogError(
                        $"Failed to fetch token list. Status Code: {response.StatusCode}"
                    );
                    tokenDataMap["error"] = "API call failed";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the token list.");
                tokenDataMap["error"] = "Exception occurred";
            }
            // Returning the parsed token data in the same format as the GetTokenListForWalletsAsync method
            return tokenDataMap;
        }
    }
}
