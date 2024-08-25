using Mouse.NET.Auth.Models;

namespace Mouse.Stick.Controllers.Auth
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;
        
        public AuthController(IAuthService authService) {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<Account> RegisterAccount ([FromBody] RegisterAccountRequest registerAccountRequest)
        {
            return await this.authService.RegisterAccount(registerAccountRequest);
        }

        [HttpPost("login")]
        public async Task<Account> LoginAccount([FromBody] LoginAccountRequest loginAccountRequest)
        {
            return await this.authService.LoginAccount(loginAccountRequest);
        }
        
        [HttpPost("change-password")]
        public async Task ChangePassword([FromBody] ChangePasswordAccountRequest changePasswordAccountRequest)
        {
            await this.authService.ChangePassword(changePasswordAccountRequest);
        }
            
    }
}
