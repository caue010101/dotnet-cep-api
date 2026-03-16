using CepSystem.Domain.Interfaces;
using System.Data;
using CepSystem.Infrastructure.Repositories;

namespace CepSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private IAddressRepository? _addressRepository;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            _transaction = _connection.BeginTransaction();
        }

        public IAddressRepository addresses =>
          _addressRepository ??= new AddressRepository(_connection, _transaction);

        public async Task CommitAsync()
        {

            try
            {
                _transaction.Commit();

            }
            catch
            {
                _transaction.Rollback();
                throw;
            }

            finally
            {
                _transaction.Dispose();

                _transaction = _connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
