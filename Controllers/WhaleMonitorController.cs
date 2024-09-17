using Microsoft.AspNetCore.Mvc;
using WalletScanner.Services;
using WalletScanner.ViewModels;

namespace WalletScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhaleMonitorController : ControllerBase
    {
        private readonly WhaleMonitorService _whaleMonitorService;

        public WhaleMonitorController(WhaleMonitorService whaleMonitorService)
        {
            _whaleMonitorService = whaleMonitorService;
        }

        [HttpGet("monitor/{token}")]
        public async Task<IActionResult> GetWhaleActivities(string token)
        {
            var activities = await _whaleMonitorService.GetWhaleActivitiesAsync(token);
            return Ok(activities);
        }

        [HttpPost("alert")]
        public async Task<IActionResult> CreateAlert(AlertViewModel alert)
        {
            await _whaleMonitorService.CreateAlertAsync(alert);
            return Ok("Alert created successfully.");
        }

        [HttpGet("alerts")]
        public async Task<IActionResult> GetAlerts()
        {
            var alerts = await _whaleMonitorService.GetAlertsAsync();
            return Ok(alerts);
        }
    }
}
