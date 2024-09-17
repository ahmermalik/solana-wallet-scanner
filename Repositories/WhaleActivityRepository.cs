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
            var tokenParam = new SqlParameter("@Token", activity.Token);
            var walletAddressParam = new SqlParameter("@WalletAddress", activity.WalletAddress);
            var amountParam = new SqlParameter("@Amount", activity.Amount);
            var activityTypeParam = new SqlParameter("@ActivityType", activity.ActivityType);
            var timestampParam = new SqlParameter("@Timestamp", activity.Timestamp);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertWhaleActivity @Token, @WalletAddress, @Amount, @ActivityType, @Timestamp",
                tokenParam, walletAddressParam, amountParam, activityTypeParam, timestampParam);
        }
    }
}
