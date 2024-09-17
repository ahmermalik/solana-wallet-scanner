using WalletScanner.Models;
using WalletScanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WalletScanner.Repositories
{
    public class AlertRepository
    {
        private readonly ApplicationDbContext _context;

        public AlertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAlertAsync(Alert alert)
        {
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Alert>> GetAllAlertsAsync()
        {
            return await _context.Alerts.ToListAsync();
        }

        // Additional methods for updating or deleting alerts can be added here
    }
}
