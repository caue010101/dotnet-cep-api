using CepSystem.Application.Dtos.Auth;

namespace CepSystem.Application.Interfaces
{

    public interface IAuthService
    {

        Task AuthUserAsync(LoginDto dto);
    }
}
