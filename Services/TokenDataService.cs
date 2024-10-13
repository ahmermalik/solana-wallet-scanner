using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WalletScanner.Models;
using WalletScanner.Repositories;
using WalletScanner.Services;

public class TokenDataService
{
    private readonly WalletRepository _walletRepository;
    private readonly BirdseyeApiService _birdseyeApiService;
    private readonly TokenRepository _tokenRepository;
    private readonly WalletHoldingRepository _walletHoldingRepository;
    private readonly ILogger<TokenDataService> _logger;

    public TokenDataService(
        WalletRepository walletRepository,
        BirdseyeApiService birdseyeApiService,
        TokenRepository tokenRepository,
        WalletHoldingRepository walletHoldingRepository,
        ILogger<TokenDataService> logger
    )
    {
        _walletRepository = walletRepository;
        _birdseyeApiService = birdseyeApiService;
        _tokenRepository = tokenRepository;
        _walletHoldingRepository = walletHoldingRepository;
        _logger = logger;
    }

    public async Task UpdateWalletTokensAsync()
    {
        try
        {
            // Step 1: Get all wallets with NetworkId from the database
            var wallets = await _walletRepository.GetAllWalletsAsync();

            if (wallets == null || !wallets.Any())
            {
                // Log warning if no wallets are found
                _logger.LogWarning("No wallets found to update tokens.");
                return;
            }

            // Step 2: Extract the list of wallet addresses for the API call
            var walletAddresses = wallets.Select(w => w.Address).ToList();

            // Step 3: Fetch token data from Birdseye API
            var tokenData = await _birdseyeApiService.GetTokenListForWalletsAsync(wallets);

            // Collections for batching
            var newTokens = new List<Token>();
            var walletHoldingsData = new List<WalletHoldingData>();

            // HashSet to track processed tokens
            var processedTokens = new HashSet<(string Address, int NetworkId)>();

            // Dictionary for wallets
            var walletDictionary = wallets.ToDictionary(w => w.Address, w => w);

            // Step 4: Process token data
            foreach (var walletEntry in tokenData)
            {
                var walletAddress = walletEntry.Key;
                var walletData = walletEntry.Value;

                if (walletData == null)
                {
                    _logger.LogWarning($"No data for wallet address: {walletAddress}");
                    continue;
                }

                if (!string.IsNullOrEmpty(walletData.Error))
                {
                    _logger.LogWarning(
                        $"Error fetching data for wallet {walletAddress}: {walletData.Error}"
                    );
                    continue;
                }

                if (walletData.Items == null || !walletData.Items.Any())
                {
                    _logger.LogWarning($"No tokens found for wallet address: {walletAddress}");
                    continue;
                }

                // Find the wallet entry that corresponds to the wallet address
                if (!walletDictionary.TryGetValue(walletAddress, out var wallet))
                {
                    _logger.LogWarning(
                        $"Wallet with address {walletAddress} not found in the database."
                    );
                    continue; // Skip if wallet is not found in the database
                }

                var networkId = wallet.NetworkId; // Get the NetworkId from the wallet

                foreach (var tokenItem in walletData.Items)
                {
                    try
                    {
                        // Check for essential fields
                        if (
                            string.IsNullOrWhiteSpace(tokenItem.Symbol)
                            || string.IsNullOrWhiteSpace(tokenItem.Name)
                        )
                        {
                            _logger.LogWarning(
                                $"Token with address {tokenItem.Address} has missing Symbol or Name. Skipping."
                            );
                            continue;
                        }

                        // Log token info
                        _logger.LogInformation(
                            $"Token Address: {tokenItem.Address}, Symbol: {tokenItem.Symbol}, Name: {tokenItem.Name}"
                        );

                        var tokenKey = (tokenItem.Address, networkId);

                        // Collect tokens to insert into Tokens table (if not exists)
                        if (!processedTokens.Contains(tokenKey))
                        {
                            // Check if token already exists in the database
                            var existingToken = await _tokenRepository.GetByAddressAsync(
                                tokenItem.Address,
                                networkId
                            );
                            if (existingToken == null)
                            {
                                var newToken = new Token
                                {
                                    Address = tokenItem.Address,
                                    Symbol = tokenItem.Symbol,
                                    Name = tokenItem.Name,
                                    Decimals = tokenItem.Decimals,
                                    Price = tokenItem.PriceUsd,
                                    NetworkId = networkId,
                                    LastUpdated = DateTime.UtcNow,
                                };
                                newTokens.Add(newToken);
                                _logger.LogInformation(
                                    $"Prepared new token for insertion: {tokenItem.Symbol} ({tokenItem.Name})"
                                );
                            }
                            processedTokens.Add(tokenKey);
                        }

                        // Prepare wallet holding data
                        var holdingData = new WalletHoldingData
                        {
                            WalletId = wallet.WalletId,
                            TokenAddress = tokenItem.Address,
                            NetworkId = networkId,
                            Balance = tokenItem.Balance ?? 0,
                            UiAmount = tokenItem.UiAmount ?? 0,
                            PriceUsd = tokenItem.PriceUsd ?? 0,
                            ValueUsd = tokenItem.ValueUsd ?? 0,
                        };
                        walletHoldingsData.Add(holdingData);

                        _logger.LogInformation(
                            $"Prepared holdings for token: {tokenItem.Symbol} in wallet: {walletAddress}"
                        );
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing token for wallet: {walletAddress}");
                        // Optionally, decide whether to continue or halt
                    }
                }
            }

            // Step 5: Insert new tokens in batches
            if (newTokens.Any())
            {
                const int tokenBatchSize = 300; // Adjust batch size as needed
                for (int i = 0; i < newTokens.Count; i += tokenBatchSize)
                {
                    var batch = newTokens.Skip(i).Take(tokenBatchSize).ToList();
                    await _tokenRepository.AddRangeAsync(batch);
                    _logger.LogInformation($"Inserted batch of {batch.Count} tokens.");
                }
            }

            // Fetch all tokens to build a dictionary for TokenId resolution
            var allTokens = await _tokenRepository.GetAllTokensAsync();
            var tokenDictionary = allTokens.ToDictionary(
                t => (t.Address, t.NetworkId),
                t => t.TokenId
            );

            // Prepare the list of WalletHoldings
            var walletHoldings = new List<WalletHolding>();

            // Resolve TokenId for each wallet holding data and create WalletHolding entities
            foreach (var holdingData in walletHoldingsData)
            {
                var tokenKey = (holdingData.TokenAddress, holdingData.NetworkId);
                if (tokenDictionary.TryGetValue(tokenKey, out var tokenId))
                {
                    var walletHolding = new WalletHolding
                    {
                        WalletId = holdingData.WalletId,
                        TokenId = tokenId,
                        Balance = holdingData.Balance,
                        UiAmount = holdingData.UiAmount,
                        PriceUsd = holdingData.PriceUsd,
                        ValueUsd = holdingData.ValueUsd,
                    };
                    walletHoldings.Add(walletHolding);
                }
                else
                {
                    _logger.LogWarning($"Token with address {holdingData.TokenAddress} not found.");
                    // Optionally skip this holding
                    continue;
                }
            }

            // Step 6: Upsert wallet holdings in batches
            if (walletHoldings.Any())
            {
                const int holdingsBatchSize = 100; // Adjust batch size as needed
                for (int i = 0; i < walletHoldings.Count; i += holdingsBatchSize)
                {
                    var batch = walletHoldings.Skip(i).Take(holdingsBatchSize).ToList();
                    await _walletHoldingRepository.UpsertWalletHoldingsAsync(batch);
                    _logger.LogInformation($"Upserted batch of {batch.Count} wallet holdings.");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating wallet tokens.");
            throw; // Re-throw if you want the exception to propagate
        }
    }
}

// Helper class for holding data before resolving TokenId
public class WalletHoldingData
{
    public int WalletId { get; set; }
    public string TokenAddress { get; set; }
    public int NetworkId { get; set; }
    public long Balance { get; set; }
    public decimal UiAmount { get; set; }
    public decimal PriceUsd { get; set; }
    public decimal ValueUsd { get; set; }
}
