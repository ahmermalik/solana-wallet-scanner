using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Data;
using WalletScanner.Models;

namespace WalletScanner.Repositories
{
    public class TokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch all tokens
        public async Task<List<Token>> GetAllTokensAsync()
        {
            return await _context.Tokens
                .Include(t => t.Network)
                .ToListAsync();
        }

        // Fetch a token by its address and network
        public async Task<Token?> GetTokenByAddressAsync(string address, int networkId)
        {
            return await _context.Tokens
                .Include(t => t.Network)
                .FirstOrDefaultAsync(t => t.Address == address && t.NetworkId == networkId);
        }

        // Method to get a token by its address
        public async Task<Token?> GetByAddressAsync(string address)
        {
            return await _context.Tokens
                .FirstOrDefaultAsync(t => t.Address == address);
        }

        // Method to add a new token
        public async Task AddAsync(Token token)
        {
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();
        }

        // Fetch token metrics
        public async Task<TokenMetric?> GetTokenMetricsAsync(int tokenId)
        {
            return await _context.TokenMetrics
                .Include(tm => tm.Token)
                .Include(tm => tm.Network)
                .FirstOrDefaultAsync(tm => tm.TokenId == tokenId);
        }
    }
}
