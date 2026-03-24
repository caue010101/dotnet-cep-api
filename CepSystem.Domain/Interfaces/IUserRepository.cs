using CepSystem.Domain.Entities;


namespace CepSystem.Domain.Interfaces
{

    public interface IUserRepository
    {

        Task<User?> GetUserByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);

    }
}
