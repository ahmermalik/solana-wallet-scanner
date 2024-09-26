// Repositories/TopTraderRepository.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Data;
using WalletScanner.Models;

namespace WalletScanner.Repositories
{
    public class TopTraderRepository
    {
        private readonly ApplicationDbContext _context;

        public TopTraderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add or update TopTraders
        public async Task AddOrUpdateTopTradersAsync(List<TopTrader> topTraders)
        {
            foreach (var trader in topTraders)
            {
                var existingTrader = await _context.TopTraders.FirstOrDefaultAsync(tt =>
                    tt.TokenId == trader.TokenId
                    && tt.WalletId == trader.WalletId
                    && tt.Period == trader.Period
                );

                if (existingTrader != null)
                {
                    // Update existing trader
                    existingTrader.Volume = trader.Volume;
                    existingTrader.TradeCount = trader.TradeCount;
                    existingTrader.TradeBuyCount = trader.TradeBuyCount;
                    existingTrader.TradeSellCount = trader.TradeSellCount;
                    existingTrader.VolumeBuy = trader.VolumeBuy;
                    existingTrader.VolumeSell = trader.VolumeSell;
                    existingTrader.UpdatedAt = trader.UpdatedAt;
                }
                else
                {
                    // Add new trader
                    _context.TopTraders.Add(trader);
                }
            }

            await _context.SaveChangesAsync();
        }

        // Fetch top traders by token
        public async Task<List<TopTrader>> GetTopTradersByTokenAsync(int tokenId)
        {
            return await _context
                .TopTraders.Include(tt => tt.Wallet)
                .Where(tt => tt.TokenId == tokenId)
                .ToListAsync();
        }
    }
}
