using Microsoft.AspNetCore.Mvc;
using wallet.application.models;
using wallet.infrastructure.Services.UserService;

namespace wallet.api.Controllers.V1
{
    //[Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Add")]
        public async Task Add([FromBody] UserRequest userRequestModel)
        {
            await _userService.Add(userRequestModel.Name);
        }

        [HttpGet("Balance/{userId}")]
        public async Task<Dictionary<string, decimal>> GetBalance(int userId)
        {
            var balance = await _userService.GetBalance(userId);
            return balance;
        }
    }
}
