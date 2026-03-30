using CepSystem.Application.Interfaces;
using CepSystem.Domain.Interfaces;
using CepSystem.Application.Dtos;
using CepSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Data.Common;

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

        public async Task<ReadUserDto?> GetUserByEmailAsync(string email)
        {

            var user = await _userRepisitory.GetUserByEmailAsync(email);

            if (user == null)
            {

                _logger.LogWarning("User not found ");
                return null;
            }

            return new ReadUserDto(

              Id: user.Id,
              Name: user.Name,
              Email: user.Email
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
                _unitOfWork.BeginTransaction();
                await _userRepisitory.AddAsync(userModel);
                _unitOfWork.Commit();

                _logger.LogInformation("User {Email} registered successfully", userDto.Email);


                return new ReadUserDto(
                    Id: userModel.Id,
                    Name: userModel.Name,
                    Email: userModel.Email
                );

            }

            catch (DbException ex) when (ex.Message.Contains("duplicate key") || ex.Message.Contains("UNIQUE"))
            {
                _unitOfWork.Rollback();
                _logger.LogWarning(ex, "Duplicate email attempt {Email}", userDto.Email);
                throw;
            }

            catch (Exception e)
            {
                _unitOfWork.Rollback();
                _logger.LogError(e, "Internal server error while registered user {Email} ");
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto userDto)
        {

            var user = await _userRepisitory.GetUserByIdAsync(userDto.Id);

            if (user == null)
            {

                _logger.LogWarning("User {Email} not found ", userDto.Email);
                throw new InvalidOperationException($"User {userDto.Email} not found ");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;

            try

            {
                _unitOfWork.BeginTransaction();
                await _userRepisitory.UpdateAsync(user);
                _unitOfWork.Commit();
                _logger.LogInformation("User {Email} updated successfully ", userDto.Email);
                return true;

            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                _logger.LogError(e, "Error updating User {Email}", userDto.Email);
                return false;

            }

        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {

            var user = await _userRepisitory.GetUserByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User {Id} not found ", id);
                return false;

            }

            try
            {
                _unitOfWork.BeginTransaction();

                await _userRepisitory.DeleteAsync(id);
                _unitOfWork.Commit();
                _logger.LogInformation("User {Id} deleted successfully ", user.Id);
                return true;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                _logger.LogError(e, "Error removing User {Id}", id);
                return false;
            }
        }
    }
}
