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

        // 2. Detect Dump Events
        public async Task<List<DumpEvent>> DetectDumpEventsAsync(string token, decimal threshold, DateTime startTime, DateTime endTime)
        {
            var tokenParam = new SqlParameter("@Token", token);
            var thresholdParam = new SqlParameter("@ThresholdAmount", threshold);
            var startTimeParam = new SqlParameter("@StartTime", startTime);
            var endTimeParam = new SqlParameter("@EndTime", endTime);

            return await _context.DumpEvents
                .FromSqlRaw("EXEC sp_DetectDumpEvents @Token, @ThresholdAmount, @StartTime, @EndTime",
                    tokenParam, thresholdParam, startTimeParam, endTimeParam)
                .ToListAsync();
        }
    }
}
