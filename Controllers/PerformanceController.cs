using Microsoft.AspNetCore.Mvc;
using WalletScanner.Services;

namespace WalletScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly MetricsService _metricsService;

        public PerformanceController(MetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        [HttpGet("metrics/{walletAddress}")]
        public async Task<IActionResult> GetPerformanceMetrics(string walletAddress)
        {
            var metrics = await _metricsService.CalculateMetricsAsync(walletAddress);
            return Ok(metrics);
        }
    }
}
