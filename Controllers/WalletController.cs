using Microsoft.AspNetCore.Mvc;
using WalletScanner.Repositories;
using WalletScanner.Services; // Import both BirdseyeApiService and TokenDataService

namespace WalletScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly BirdseyeApiService _birdseyeApiService;
        private readonly TokenDataService _tokenDataService;
        private readonly WalletRepository _walletRepository;

        // Constructor injection for both BirdseyeApiService and TokenDataService
        public WalletController(
            BirdseyeApiService birdseyeApiService,
            TokenDataService tokenDataService,
            WalletRepository walletRepository
        )
        {
            _birdseyeApiService = birdseyeApiService;
            _tokenDataService = tokenDataService;
            _walletRepository = walletRepository;
        }

        [HttpPost("wallets/token-lists")]
        public async Task<IActionResult> GetTokenListsForWallets()
        {
            var wallets = await _walletRepository.GetAllWalletsAsync();
            var response = await _birdseyeApiService.GetTokenListForWalletsAsync(wallets);
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
