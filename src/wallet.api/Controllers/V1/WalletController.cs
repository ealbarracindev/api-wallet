using Microsoft.AspNetCore.Mvc;
using wallet.application.Models.Requests;
using wallet.infrastructure.Helpers.CurrencyHelper;
using wallet.infrastructure.Services.WalletService;

namespace wallet.api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ICurrencyHelper _currencyHelper;
        private readonly IConfiguration _configuration;

        public WalletController(IWalletService walletService, ICurrencyHelper currencyHelper, IConfiguration configuration)
        {
            _walletService = walletService;
            _currencyHelper = currencyHelper;
            _configuration = configuration;
        }

        [HttpPost("Add")]
        public async Task Add([FromBody] WalletRequest walletRequest)
        {

            await _walletService.Add(walletRequest.UserId, walletRequest.Currency);

        }

        [HttpPost("TopUp")]
        public async Task TopUp([FromBody] WalletRequest walletRequest)
        {
            await _walletService.TopUp(walletRequest.UserId, walletRequest.Currency, walletRequest.Amount);
        }

        [HttpPost("Withdraw")]
        public async Task Withdraw([FromBody] WalletRequest walletRequest)
        {
            await _walletService.Withdraw(walletRequest.UserId, walletRequest.Currency, walletRequest.Amount);
        }

        [HttpPost("Transfer")]
        public async Task Transfer([FromBody] WalletTransferRequest walletTransferRequest)
        {
            await _walletService.Transfer(walletTransferRequest.UserId, walletTransferRequest.FromCurrency, walletTransferRequest.ToCurrency, walletTransferRequest.Amount, _currencyHelper);
        }
    }
}
