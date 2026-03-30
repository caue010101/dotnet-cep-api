using CepSystem.Domain.Entities;


namespace CepSystem.Application.Interfaces
{

    public interface IJwtService
    {

        string GenerateToken(User user);
    }
}
