using WalletScanner.Models;
using WalletScanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public class WalletHoldingRepository
{
    private readonly ApplicationDbContext _context;

    public WalletHoldingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Method to upsert wallet holdings (insert if not exists, update if exists)
    public async Task UpsertWalletHoldingAsync(
        string walletAddress,
        string tokenAddress,
        long balance,
        decimal uiAmount,
        decimal priceUsd,
        decimal valueUsd)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(w => w.Address == walletAddress);
        var token = await _context.Tokens
            .FirstOrDefaultAsync(t => t.Address == tokenAddress);

        if (wallet == null || token == null)
        {
            // Log or handle missing wallet or token
            return;
        }

        var walletHolding = await _context.WalletHoldings
            .FirstOrDefaultAsync(wh => wh.WalletId == wallet.WalletId && wh.TokenId == token.TokenId);

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
                ValueUsd = valueUsd
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

    // Add other methods for interacting with WalletHoldings...
}
