using CepSystem.Application.Dtos;

namespace CepSystem.Application.Interfaces
{

    public interface IUserService
    {

        Task<ReadUserDto?> GetUserByIdAsync(Guid id);
        Task<ReadUserDto?> AddUserAsync(CreateUserDto userDto);
        Task<bool> UpdateUserAsync(UpdateUserDto userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
