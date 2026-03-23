using System.Data;


namespace CepSystem.Domain.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        public IDbTransaction Transaction { get; }
        void Commit();
        void BeginTransaction();
        void Rollback();
    }

}
