using CepSystem.Application.Interfaces;
using CepSystem.Domain.Interfaces;
using CepSystem.Application.Dtos;
using CepSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using BCrypt.Net;

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

        public async Task<ReadUserDto?> AddUserAsync(CreateUserDto userDto)
        {


            var userModel = new User
            {

                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };

            try
            {
                await _userRepisitory.AddAsync(userModel);
                _unitOfWork.Commit();

                _logger.LogInformation("User {Email} registered successfully", userDto.Email);


                return new ReadUserDto(
                    Id: userModel.Id,
                    Name: userModel.Name,
                    Email: userModel.Email
                );

            }

            catch (Exception e)
            {
                _unitOfWork.Rollback();
                _logger.LogError(e, "Internal server error while registered user {Email} ");
                throw;
            }


        }
    }
}
