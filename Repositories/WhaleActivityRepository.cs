using System;
using System.Data;
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

        // 1. Insert Whale Activity
        public async Task InsertWhaleActivityAsync(WhaleActivity activity)
        {
            // Ensure that the Token and Wallet navigation properties are loaded
            if (activity.Token == null || activity.Wallet == null)
            {
                // Optionally, load the Token and Wallet from the database
                activity = await _context
                    .WhaleActivities.Include(wa => wa.Token)
                    .Include(wa => wa.Wallet)
                    .FirstOrDefaultAsync(wa => wa.WhaleActivityId == activity.WhaleActivityId);

                if (activity == null || activity.Token == null || activity.Wallet == null)
                {
                    throw new InvalidOperationException(
                        "Token and Wallet must be loaded in WhaleActivity."
                    );
                }
            }

            var tokenParam = new SqlParameter("@Token", activity.Token.Symbol); // Use Token.Symbol or Token.Address
            var walletAddressParam = new SqlParameter("@WalletAddress", activity.Wallet.Address);
            var amountParam = new SqlParameter("@Amount", activity.Amount);
            var activityTypeParam = new SqlParameter("@ActivityType", activity.ActivityType);
            var timestampParam = new SqlParameter("@Timestamp", activity.Timestamp);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertWhaleActivity @Token, @WalletAddress, @Amount, @ActivityType, @Timestamp",
                tokenParam,
                walletAddressParam,
                amountParam,
                activityTypeParam,
                timestampParam
            );
        }
    }
}
