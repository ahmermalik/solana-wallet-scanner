using System.Threading.Tasks;
using WalletScanner.Models;
using Microsoft.Extensions.Logging;

namespace WalletScanner.Services
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async Task SendNotificationAsync(int userId, WhaleActivity activity)
        {
            // TODO: Implement actual notification logic (e.g., send email or SMS)
            _logger.LogInformation($"Sending notification to user {userId} for activity: {activity.ActivityType} on token {activity.Token}");
            await Task.CompletedTask;
        }
    }
}
