using Microsoft.AspNetCore.Mvc;
using CepSystem.Application.Interfaces;
using CepSystem.Application.Dtos.Auth;
using Microsoft.AspNetCore.RateLimiting;

namespace CepSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {

        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        [EnableRateLimiting("login")]


        public async Task<IActionResult> LoginAuth([FromBody] LoginDto dto)
        {

            var token = await _authService.AuthUserAsync(dto);

            if (token == null)
            {

                return Unauthorized("Error, invalid credendials ");
            }

            return Ok(new { token });
        }

    }
}
