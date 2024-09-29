using Microsoft.AspNetCore.Mvc;
using WalletScanner.Services;  // Import both BirdseyeApiService and TokenDataService

namespace WalletScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly BirdseyeApiService _birdseyeApiService;
        private readonly TokenDataService _tokenDataService;

        // Constructor injection for both BirdseyeApiService and TokenDataService
        public WalletController(BirdseyeApiService birdseyeApiService, TokenDataService tokenDataService)
        {
            _birdseyeApiService = birdseyeApiService;
            _tokenDataService = tokenDataService;
        }

        [HttpPost("wallets/token-lists")]
        public async Task<IActionResult> GetTokenListsForWallets([FromBody] List<string> walletAddresses)
        {
            var response = await _birdseyeApiService.GetTokenListForWalletsAsync(walletAddresses);
            return Ok(response);
        }

        [HttpPost("update-tokens")]
        public async Task<IActionResult> UpdateWalletTokens()
        {
            await _tokenDataService.UpdateWalletTokensAsync();
            return Ok("Wallet tokens updated successfully.");
        }
    }
}
