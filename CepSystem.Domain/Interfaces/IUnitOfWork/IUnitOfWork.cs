
namespace CepSystem.Domain.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {

        IAddressRepository addresses { get; }

        Task CommitAsync();
    }

}
