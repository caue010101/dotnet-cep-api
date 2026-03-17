using CepSystem.Application.Dtos;

namespace CepSystem.Application.Interfaces
{

    public interface IAddressService
    {
        Task<AddressDto> GetByZipCodeAsync(string zipCode);
        Task<AddressDto> AddAsync(CreateZipCodeDto dto);
    }
}
