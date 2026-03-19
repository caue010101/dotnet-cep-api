using CepSystem.Application.Dtos;

namespace CepSystem.Application.Interfaces
{

    public interface IAddressService
    {
        Task<ResponseAddressDto?> GetByZipCodeAsync(string zipCode);
    }
}
