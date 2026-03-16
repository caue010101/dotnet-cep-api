using CepSystem.Domain.Entities;


namespace CepSystem.Domain.Interfaces
{

    public interface IAdressRepository
    {

        Task<Address?> GetByZipCodeAsync(string zipCode);
        Task<bool> AddAsync(Address address);
    }
}
