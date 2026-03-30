using CepSystem.Application.Interfaces;
using CepSystem.Application.Dtos.Auth;
using Microsoft.Extensions.Logging;

namespace CepSystem.Application.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;
        private readonly IJwtService _jwtService;

        public AuthService(IUserService userService, ILogger<AuthService> logger, IJwtService jwtService)
        {
            this._userService = userService;
            this._logger = logger;
            this._jwtService = jwtService;
        }

        public async Task<string?> AuthUserAsync(LoginDto dto)
        {

            var user = await _userService.GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                _logger.LogWarning("User not found, email invalid ");
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                _logger.LogCritical("Error, invalid credentials ");
                return null;
            }

            _logger.LogInformation("User {Email} logged in succefully ", dto.Email);

            var token = _jwtService.GenerateToken(user);

            return token;


        }
    }
}
