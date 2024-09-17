using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletScanner.Services;

namespace WalletScanner.BackgroundServices
{
    public class WhaleMonitorBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WhaleMonitorBackgroundService> _logger;
        private readonly TimeSpan _delay = TimeSpan.FromMinutes(5); // Adjust as needed

        public WhaleMonitorBackgroundService(IServiceProvider serviceProvider, ILogger<WhaleMonitorBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WhaleMonitorBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("WhaleMonitorBackgroundService is running.");

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var whaleMonitorService = scope.ServiceProvider.GetRequiredService<WhaleMonitorService>();
                        await whaleMonitorService.ProcessWhaleActivitiesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing whale activities.");
                }

                await Task.Delay(_delay, stoppingToken);
            }

            _logger.LogInformation("WhaleMonitorBackgroundService is stopping.");
        }
    }
}
