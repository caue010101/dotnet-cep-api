using CepSystem.Domain.Entities;


namespace CepSystem.Domain.Interfaces
{

    public interface IAddressRepository
    {

        Task<Address?> GetByZipCodeAsync(string zipCode);
        Task AddAsync(Address address);
    }
}
