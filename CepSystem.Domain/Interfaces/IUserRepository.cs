using CepSystem.Domain.Entities;


namespace CepSystem.Domain.Interfaces
{

    public interface IUserRepository
    {

        Task<User?> GetUserByIdAsync(Guid id);

    }
}
