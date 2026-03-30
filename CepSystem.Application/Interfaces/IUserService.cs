using CepSystem.Application.Dtos;
using CepSystem.Domain.Entities;

namespace CepSystem.Application.Interfaces
{

    public interface IUserService
    {

        Task<ReadUserDto?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<ReadUserDto?> AddUserAsync(CreateUserDto userDto);
        Task<bool> UpdateUserAsync(UpdateUserDto userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
