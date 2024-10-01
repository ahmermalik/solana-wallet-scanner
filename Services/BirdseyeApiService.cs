using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using WalletScanner.Helpers;

namespace WalletScanner.Services
{
    public class BirdseyeApiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BirdseyeApiService> _logger;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly SemaphoreSlim _semaphore;
        private readonly int _requestLimitPerSecond = 13; // Approx. 13 requests per second (1000 requests/minute limit)
        private readonly int _delayBetweenBatchesInMs = 1000; // 1 second delay between batches

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

            // Initialize Polly retry policy
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => (int)r.StatusCode == 429)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (response, timespan, retryAttempt, context) =>
                    {
                        _logger.LogWarning(
                            $"Retry {retryAttempt} after {timespan.Seconds} seconds due to 429."
                        );
                    }
                );

            // Limit concurrent requests
            _semaphore = new SemaphoreSlim(_requestLimitPerSecond); // Adjust based on rate limits
        }

        public async Task<Dictionary<string, object>> GetTokenListForWalletsAsync(
            List<string> walletAddresses
        )
        {
            var walletDataMap = new ConcurrentDictionary<string, object>();
            var tasks = new List<Task>();

            foreach (var walletAddress in walletAddresses)
            {
                await _semaphore.WaitAsync();

                tasks.Add(
                    Task.Run(async () =>
                    {
                        try
                        {
                            string endpoint =
                                $"https://public-api.birdeye.so/v1/wallet/token_list?wallet={walletAddress}";
                            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(
                                () => _httpClient.GetAsync(endpoint)
                            );

                            var content = await response.Content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                if (
                                    response.Content.Headers.ContentType.MediaType
                                    == "application/json"
                                )
                                {
                                    JObject json = JObject.Parse(content);

                                    var data = json["data"];
                                    if (data != null)
                                    {
                                        var items =
                                            data["items"]?.ToObject<List<JObject>>()
                                            ?? new List<JObject>();

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
                                            $"No 'data' field for wallet {walletAddress}"
                                        );
                                        walletDataMap[walletAddress] = new { error = "No data" };
                                    }
                                }
                                else
                                {
                                    _logger.LogError(
                                        $"Unexpected content type: {response.Content.Headers.ContentType.MediaType}"
                                    );
                                    walletDataMap[walletAddress] = new
                                    {
                                        error = "Bad content type",
                                    };
                                }
                            }
                            else
                            {
                                _logger.LogError(
                                    $"Failed for wallet: {walletAddress}. Status: {response.StatusCode}"
                                );
                                walletDataMap[walletAddress] = new { error = "API failed" };
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error fetching wallet: {walletAddress}");
                            walletDataMap[walletAddress] = new { error = "Exception" };
                        }
                        finally
                        {
                            _semaphore.Release();
                        }
                    })
                );
                if (tasks.Count % _requestLimitPerSecond == 0)
                {
                    await Task.Delay(_delayBetweenBatchesInMs);
                }
            }
            await Task.WhenAll(tasks);
            return walletDataMap.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public async Task<Dictionary<string, object>> GetTrendingTokensAsync()
        {
            var trendingTokenDataMap = new Dictionary<string, object>();
            string endpoint = "https://public-api.birdeye.so/defi/token_trending";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                request.Headers.Add("x-chain", "solana");

                HttpResponseMessage response = await _retryPolicy.ExecuteAsync(
                    () => _httpClient.SendAsync(request)
                );

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
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
                        trendingTokenDataMap["updateTime"] = DateTimeHelper.UnixTimeStampToDateTime(
                            data["updateTime"]?.ToObject<long>() ?? 0
                        );
                        trendingTokenDataMap["total"] = data["total"]?.ToObject<int>();
                    }
                    else
                    {
                        _logger.LogWarning("No 'data' in trending tokens.");
                        trendingTokenDataMap["error"] = "No data";
                    }
                }
                else
                {
                    _logger.LogError($"Failed trending tokens. Status: {response.StatusCode}");
                    trendingTokenDataMap["error"] = "API failed";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching trending tokens.");
                trendingTokenDataMap["error"] = "Exception";
            }

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
                request.Headers.Add("x-chain", "solana");

                HttpResponseMessage response = await _retryPolicy.ExecuteAsync(
                    () => _httpClient.SendAsync(request)
                );

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
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
                                lastTradeUnixTime = DateTimeHelper.UnixTimeStampToDateTime(
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
                        _logger.LogWarning("No 'data' in token list.");
                        tokenDataMap["error"] = "No data";
                    }
                }
                else
                {
                    _logger.LogError($"Failed token list. Status: {response.StatusCode}");
                    tokenDataMap["error"] = "API failed";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching token list.");
                tokenDataMap["error"] = "Exception";
            }
            return tokenDataMap;
        }
    }
}
