using CepSystem.Application.Dtos.Auth;

namespace CepSystem.Application.Interfaces
{

    public interface IAuthService
    {

        Task<string?> AuthUserAsync(LoginDto dto);
    }
}
