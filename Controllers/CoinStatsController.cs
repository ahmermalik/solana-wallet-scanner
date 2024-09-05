using Microsoft.AspNetCore.Mvc;
using WalletScanner.Services;

namespace WalletScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoinStatsController : ControllerBase
    {
        private readonly CoinStatsService _coinStatsService;

        public CoinStatsController(CoinStatsService coinStatsService)
        {
            _coinStatsService = coinStatsService;
        }

        [HttpGet("stats/{token}")]
        public async Task<IActionResult> GetCoinStats(string token)
        {
            var stats = await _coinStatsService.GetCoinStatsAsync(token);
            return Ok(stats);
        }
    }
}
