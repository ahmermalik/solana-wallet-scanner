using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Data;
using WalletScanner.Models;

namespace WalletScanner.Repositories
{
    public class DumpEventRepository
    {
        private readonly ApplicationDbContext _context;

        public DumpEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Detect Dump Events
        public async Task<List<DumpEvent>> DetectDumpEventsAsync(int tokenId, decimal threshold, DateTime startTime, DateTime endTime)
        {
            var tokenIdParam = new SqlParameter("@TokenId", tokenId);
            var thresholdParam = new SqlParameter("@ThresholdAmount", threshold);
            var startTimeParam = new SqlParameter("@StartTime", startTime);
            var endTimeParam = new SqlParameter("@EndTime", endTime);

            return await _context.DumpEvents
                .FromSqlRaw("EXEC sp_DetectDumpEvents @TokenId, @ThresholdAmount, @StartTime, @EndTime",
                    tokenIdParam, thresholdParam, startTimeParam, endTimeParam)
                .Include(de => de.Token)
                .Include(de => de.Network)
                .ToListAsync();
        }
    }
}
