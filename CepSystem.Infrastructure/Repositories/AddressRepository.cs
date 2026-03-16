using Dapper;
using System.Data;
using CepSystem.Domain.Entities;
using CepSystem.Domain.Interfaces;

namespace CepSystem.Infrastructure.Repositories
{



    public class AddressRepository : IAddressRepository
    {

        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public AddressRepository(IDbConnection connection, IDbTransaction transaction)
        {
            this._connection = connection;
            this._transaction = transaction;
        }

        public async Task<Address?> GetByZipCodeAsync(string zipCode)
        {

            const string sql = @"SELECT 
                  zip_code AS ZipCode,
                  street AS Street,
                  complement AS Complement,
                  neighborhood AS Neighborhood,
                  city AS City,
                  state AS State,
                  area_code AS AreaCode,
                  created_at AS CreatedAt,
                  updated_at AS UpdatedAt
                      FROM address WHERE zip_code = @ZipCode";

            return await _connection.QueryFirstOrDefaultAsync<Address>(sql, new { ZipCode = zipCode },
                transaction: _transaction);
        }

        public async Task AddAsync(Address address)
        {


            const string sql = @"INSERT INTO address
              (zip_code, street, complement, neighborhood, city, state, area_code, created_at, updated_at)
                VALUES(@ZipCode, @Street, @Complement, @Neighborhood, @City, @State, @AreaCode, @CreatedAt, @UpdatedAt)";


            await _connection.ExecuteAsync
              (sql, address, transaction: _transaction);


        }
    }
}
