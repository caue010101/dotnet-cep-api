using CepSystem.Application.Interfaces;
using CepSystem.Domain.Interfaces;
using CepSystem.Application.Dtos;
using Microsoft.Extensions.Logging;

namespace CepSystem.Application.Services
{

    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepisitory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            this._userRepisitory = userRepository;
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task<ReadUserDto?> GetUserByIdAsync(Guid id)
        {

            var userModel = await _userRepisitory.GetUserByIdAsync(id);

            if (userModel == null)
            {
                _logger.LogInformation("User not found ");
                return null;
            }

            return new ReadUserDto(
              Id: userModel.Id,
              Name: userModel.Name,
              Email: userModel.Email
            );

        }
    }
}
