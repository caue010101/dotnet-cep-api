using CepSystem.Application.Dtos;
using CepSystem.Application.Interfaces;
using CepSystem.Domain.Interfaces;
using Serilog;
using Microsoft.Extensions.Logging;
using CepSystem.Domain.Entities;


namespace CepSystem.Application.Services
{

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger)
        {
            this._addressRepository = addressRepository;
            this._logger = logger;
        }

        public async Task<ResponseAddressDto> GetByZipCodeAsync(string zipCode)
        {

            var adress = await _addressRepository.GetByZipCodeAsync(zipCode);

            if (adress == null)
            {
                _logger.LogWarning("zipcode invalid, adress not found ");
                return null;

            }

            return new ResponseAddressDto(

              Street: adress.Street,
              Neighborhood: adress.Neighborhood,
              City: adress.City,
              State: adress.State,
              AreaCode: adress.AreaCode,
              CreatedAt: adress.CreatedAt,
              UpdatedAt: adress.UpdatedAt
            );


        }
    }

}
