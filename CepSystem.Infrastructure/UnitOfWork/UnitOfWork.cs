using CepSystem.Domain.Interfaces;
using System.Data;

namespace CepSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;

        public IDbTransaction? Transaction => _transaction;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;

        }

        public void BeginTransaction()
        {

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {

            try
            {
                _transaction.Commit();

            }
            catch
            {
                Rollback();
                throw;
            }

            finally
            {
                _transaction.Dispose();

                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
