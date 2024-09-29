using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Data;
using WalletScanner.Models;

namespace WalletScanner.Repositories
{
    public class WhaleActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public WhaleActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Insert Whale Activity
        public async Task InsertWhaleActivityAsync(WhaleActivity activity)
        {
            // Ensure that the Token and Wallet navigation properties are loaded
            if (activity.Token == null || activity.Wallet == null)
            {
                throw new InvalidOperationException("Token and Wallet must be provided in WhaleActivity.");
            }

            // Set NetworkId based on Token or Wallet
            activity.NetworkId = activity.Token.NetworkId;

            _context.WhaleActivities.Add(activity);
            await _context.SaveChangesAsync();
        }

        // Get Whale Activities by Token and Network
        public async Task<List<WhaleActivity>> GetWhaleActivitiesByTokenAsync(int tokenId, int networkId)
        {
            return await _context.WhaleActivities
                .Include(wa => wa.Token)
                .Include(wa => wa.Wallet)
                .Include(wa => wa.Network)
                .Where(wa => wa.TokenId == tokenId && wa.NetworkId == networkId)
                .ToListAsync();
        }
    }
}
