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
            var tokenData = await _birdseyeApiService.GetTokenListForWalletsAsync(walletAddresses);

            // Step 4: Insert or update the token and wallet holdings data
            foreach (var wallet in tokenData)
            {
                var walletAddress = wallet.Key;
                var walletTokens = wallet.Value as dynamic;

                if (walletTokens?.items == null)
                {
                    _logger.LogWarning($"No tokens found for wallet address: {walletAddress}");
                    continue;
                }

                // Find the wallet entry that corresponds to the wallet address
                var walletEntry = wallets.FirstOrDefault(w => w.Address == walletAddress);
                if (walletEntry == null)
                {
                    _logger.LogWarning(
                        $"Wallet with address {walletAddress} not found in the database."
                    );
                    continue; // Skip if wallet is not found in the database
                }

                var networkId = walletEntry.NetworkId; // Get the NetworkId from the wallet

                foreach (var token in walletTokens.items)
                {
                    try
                    {
                        // Parse data from API with null handling
                        var tokenAddress = token.address as string;
                        var tokenSymbol = token.symbol as string;
                        var tokenName = token.name as string;
                        var tokenDecimals = token.decimals != null ? (int?)token.decimals : null;
                        var tokenPriceUsd =
                            token.priceUsd != null ? (decimal?)token.priceUsd : null;
                        var tokenValueUsd =
                            token.valueUsd != null ? (decimal?)token.valueUsd : null;

                        // Log token info, handle nulls
                        _logger.LogInformation(
                            $"Token Address: {tokenAddress}, Symbol: {tokenSymbol ?? "NULL"}, Name: {tokenName ?? "NULL"}"
                        );

                        // Check for essential fields
                        if (
                            string.IsNullOrWhiteSpace(tokenSymbol)
                            || string.IsNullOrWhiteSpace(tokenName)
                        )
                        {
                            _logger.LogWarning(
                                $"Token with address {tokenAddress} has missing Symbol or Name. Skipping."
                            );
                            continue;
                        }

                        // Step 5: Insert token into Tokens table (if not exists)
                        var existingToken = await _tokenRepository.GetByAddressAsync(tokenAddress);
                        if (existingToken == null)
                        {
                            var newToken = new Token
                            {
                                Address = tokenAddress,
                                Symbol = tokenSymbol,
                                Name = tokenName,
                                Decimals = tokenDecimals,
                                Price = tokenPriceUsd,
                                NetworkId = networkId, // Use the correct NetworkId from the wallet
                                LastUpdated = DateTime.UtcNow,
                            };
                            await _tokenRepository.AddAsync(newToken);
                            _logger.LogInformation($"Added new token: {tokenSymbol} ({tokenName})");
                        }

                        // Step 6: Insert or update wallet holdings
                        var balance = token.balance != null ? (long?)token.balance : null;
                        var uiAmount = token.uiAmount != null ? (decimal?)token.uiAmount : null;

                        // Handle potential nulls or assign default values
                        await _walletHoldingRepository.UpsertWalletHoldingAsync(
                            walletAddress,
                            tokenAddress,
                            balance ?? 0,
                            uiAmount ?? 0,
                            tokenPriceUsd ?? 0,
                            tokenValueUsd ?? 0
                        );
                        _logger.LogInformation(
                            $"Updated holdings for token: {tokenSymbol} in wallet: {walletAddress}"
                        );
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing token for wallet: {walletAddress}");
                        // Optionally, decide whether to continue or halt
                    }
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
