using WalletScanner.Repositories;
using WalletScanner.Models;
using WalletScanner.Services;

public class TokenDataService
{
    private readonly WalletRepository _walletRepository;
    private readonly BirdseyeApiService _birdseyeApiService;
    private readonly TokenRepository _tokenRepository;
    private readonly WalletHoldingRepository _walletHoldingRepository;

    public TokenDataService(
        WalletRepository walletRepository,
        BirdseyeApiService birdseyeApiService,
        TokenRepository tokenRepository,
        WalletHoldingRepository walletHoldingRepository
    )
    {
        _walletRepository = walletRepository;
        _birdseyeApiService = birdseyeApiService;
        _tokenRepository = tokenRepository;
        _walletHoldingRepository = walletHoldingRepository;
    }

    public async Task UpdateWalletTokensAsync()
    {
        // Step 1: Get all wallets from the database
        var walletAddresses = await _walletRepository.GetAllWalletAddressesAsync();

        if (walletAddresses == null || !walletAddresses.Any())
        {
            // Log or throw error if no wallets are found
            return;
        }

        // Step 2: Fetch token data from Birdseye API
        var tokenData = await _birdseyeApiService.GetTokenListForWalletsAsync(walletAddresses);

        // Step 3: Insert or update the token and wallet holdings data
        foreach (var wallet in tokenData)
        {
            var walletAddress = wallet.Key;
            var walletTokens = wallet.Value as dynamic;

            if (walletTokens?.items == null) continue;

            foreach (var token in walletTokens.items)
            {
                // Example parsing data from API
                var tokenAddress = (string)token.address;
                var tokenSymbol = (string)token.symbol;
                var tokenName = (string)token.name;
                var tokenDecimals = (int)token.decimals;
                var tokenPriceUsd = (decimal)token.priceUsd;
                var tokenValueUsd = (decimal)token.valueUsd;

                // Step 4: Insert token into Tokens table (if not exists)
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
                        LastUpdated = DateTime.UtcNow
                    };
                    await _tokenRepository.AddAsync(newToken);
                }

                // Step 5: Insert or update wallet holdings
                var balance = (long)token.balance;
                var uiAmount = (decimal)token.uiAmount;

                await _walletHoldingRepository.UpsertWalletHoldingAsync(
                    walletAddress,
                    tokenAddress,
                    balance,
                    uiAmount,
                    tokenPriceUsd,
                    tokenValueUsd
                );
            }
        }
    }
}
