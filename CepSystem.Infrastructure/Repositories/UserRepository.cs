using CepSystem.Domain.Interfaces;
using System.Data;
using CepSystem.Domain.Entities;
using Dapper;

namespace CepSystem.Infrastructure.Repositories
{

    public class UserRepository : IUserRepository
    {

        private readonly IDbConnection _connection;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IDbConnection connection, IUnitOfWork unitOfWork)
        {
            this._connection = connection;
            this._unitOfWork = unitOfWork;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {

            const string sql = @"
                          SELECT 
                          id AS Id,
                          name AS Name,
                          email AS Email,
                          password_hash AS PasswordHash
                           FROM users
                           WHERE id = @Id";

            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id },
                transaction: _unitOfWork.Transaction);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {

            const string sql = @"
                      SELECT
                      id AS Id,
                      name AS Name,
                      email AS Email,
                      password_hash AS PasswordHash
                        FROM users
                        WHERE email = @Email";

            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email },
                transaction: _unitOfWork.Transaction);
        }

        public async Task AddAsync(User user)
        {

            const string sql = @"INSERT INTO users (id, name, email, password_hash)
              VALUES (@Id, @Name, @Email, @PasswordHash)";

            await _connection.ExecuteAsync(sql, user,
                transaction: _unitOfWork.Transaction);
        }

        public async Task UpdateAsync(User user)
        {

            const string sql = @"UPDATE users SET name = @Name, email = @Email 
              WHERE id = @Id";

            await _connection.ExecuteAsync(sql, user,
                transaction: _unitOfWork.Transaction);

        }

        public async Task DeleteAsync(Guid id)
        {

            const string sql = "DELETE FROM users WHERE id = Id";

            await _connection.ExecuteAsync(sql, new { Id = id },
                transaction: _unitOfWork.Transaction);
        }
    }
}
