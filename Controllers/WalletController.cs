using Microsoft.AspNetCore.Mvc;
using WalletScanner.Services;

namespace WalletScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly BirdseyeApiService _birdseyeApiService;

        public WalletController(BirdseyeApiService birdseyeApiService)
        {
            _birdseyeApiService = birdseyeApiService;
        }

        [HttpPost("wallets/token-lists")]
        public async Task<IActionResult> GetTokenListsForWallets([FromBody] List<string> walletAddresses)
        {
            var response = await _birdseyeApiService.GetTokenListForWalletsAsync(walletAddresses);
            return Ok(response);
        }
    }
}
