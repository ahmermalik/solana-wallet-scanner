using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Data;
using WalletScanner.Models;

public class WalletHoldingRepository
{
    private readonly ApplicationDbContext _context;

    public WalletHoldingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Existing method (if still needed)
    public async Task UpsertWalletHoldingAsync(
        string walletAddress,
        string tokenAddress,
        long balance,
        decimal uiAmount,
        decimal priceUsd,
        decimal valueUsd
    )
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Address == walletAddress);
        var token = await _context.Tokens.FirstOrDefaultAsync(t => t.Address == tokenAddress);

        if (wallet == null || token == null)
        {
            // Log or handle missing wallet or token
            return;
        }

        var walletHolding = await _context.WalletHoldings.FirstOrDefaultAsync(wh =>
            wh.WalletId == wallet.WalletId && wh.TokenId == token.TokenId
        );

        if (walletHolding == null)
        {
            // Insert new wallet holding if not exists
            walletHolding = new WalletHolding
            {
                WalletId = wallet.WalletId,
                TokenId = token.TokenId,
                Balance = balance,
                UiAmount = uiAmount,
                PriceUsd = priceUsd,
                ValueUsd = valueUsd,
            };
            _context.WalletHoldings.Add(walletHolding);
        }
        else
        {
            // Update existing wallet holding
            walletHolding.Balance = balance;
            walletHolding.UiAmount = uiAmount;
            walletHolding.PriceUsd = priceUsd;
            walletHolding.ValueUsd = valueUsd;
        }

        await _context.SaveChangesAsync();
    }

    // New method for batch upsert
    public async Task UpsertWalletHoldingsAsync(IEnumerable<WalletHolding> holdings)
    {
        // Prepare lists for new and existing holdings
        var newHoldings = new List<WalletHolding>();
        var updatedHoldings = new List<WalletHolding>();

        // Get unique WalletIds and TokenIds from the holdings
        var walletIds = holdings.Select(h => h.WalletId).Distinct().ToList();
        var tokenIds = holdings.Select(h => h.TokenId).Distinct().ToList();

        // Fetch existing holdings in one query
        var existingHoldings = await _context
            .WalletHoldings.Where(wh =>
                walletIds.Contains(wh.WalletId) && tokenIds.Contains(wh.TokenId)
            )
            .ToListAsync();

        // Create a lookup for existing holdings
        var existingHoldingsLookup = existingHoldings.ToDictionary(wh => (wh.WalletId, wh.TokenId));

        foreach (var holding in holdings)
        {
            var key = (holding.WalletId, holding.TokenId);
            if (existingHoldingsLookup.TryGetValue(key, out var existingHolding))
            {
                // Update existing holding
                existingHolding.Balance = holding.Balance;
                existingHolding.UiAmount = holding.UiAmount;
                existingHolding.PriceUsd = holding.PriceUsd;
                existingHolding.ValueUsd = holding.ValueUsd;
                updatedHoldings.Add(existingHolding);
            }
            else
            {
                // Add new holding
                newHoldings.Add(holding);
            }
        }

        // Bulk insert new holdings
        if (newHoldings.Any())
        {
            await _context.WalletHoldings.AddRangeAsync(newHoldings);
        }

        // Save changes for both new and updated holdings
        await _context.SaveChangesAsync();
    }
}
