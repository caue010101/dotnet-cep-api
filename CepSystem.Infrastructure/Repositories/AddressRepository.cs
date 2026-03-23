using Dapper;
using System.Data;
using CepSystem.Domain.Entities;
using CepSystem.Domain.Interfaces;

namespace CepSystem.Infrastructure.Repositories
{



    public class AddressRepository : IAddressRepository
    {

        private readonly IDbConnection _connection;
        private readonly IUnitOfWork _unitOfWork;

        public AddressRepository(IDbConnection connection, IUnitOfWork unitOfWork)
        {
            this._connection = connection;
            this._unitOfWork = unitOfWork;

        }

        public async Task<Address?> GetByZipCodeAsync(string zipCode)
        {

            const string sql = @"SELECT 
                  zip_code AS ZipCode,
                  street AS Street,
                  neighborhood AS Neighborhood,
                  city AS City,
                  state AS State,
                  area_code AS AreaCode,
                  created_at AS CreatedAt,
                  updated_at AS UpdatedAt
                      FROM addresses WHERE zip_code = @ZipCode";

            return await _connection.QueryFirstOrDefaultAsync<Address>(sql, new { ZipCode = zipCode },
                transaction: _unitOfWork.Transaction);
        }

        public async Task AddAsync(Address address)
        {


            const string sql = @"INSERT INTO addresses
              (zip_code, street, neighborhood, city, state, area_code, created_at, updated_at)
                VALUES(@ZipCode, @Street,@Neighborhood, @City, @State, @AreaCode, @CreatedAt, @UpdatedAt)";


            await _connection.ExecuteAsync
              (sql, address, transaction: _unitOfWork.Transaction);


        }
    }
}
