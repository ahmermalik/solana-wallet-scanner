// Repositories/TokenRepository.cs
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
            return await _context.Tokens.ToListAsync();
        }

        // Fetch a token by its address
        public async Task<Token> GetTokenByAddressAsync(string address)
        {
            return await _context.Tokens.FirstOrDefaultAsync(t => t.Address == address);
        }

        // Fetch token metrics (assuming a method exists in your repository)
        public async Task<TokenMetric> GetTokenMetricsAsync(int tokenId)
        {
            return await _context.TokenMetrics.FirstOrDefaultAsync(tm => tm.TokenId == tokenId);
        }
    }
}
