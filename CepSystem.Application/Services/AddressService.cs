using CepSystem.Application.Dtos;
using CepSystem.Application.Interfaces;
using CepSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using CepSystem.Domain.Entities;


namespace CepSystem.Application.Services
{

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;
        private readonly IViaCepService _cepService;

        public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger, IViaCepService cepService)
        {
            this._addressRepository = addressRepository;
            this._logger = logger;
            this._cepService = cepService;
        }

        public async Task<ResponseAddressDto> GetByZipCodeAsync(string zipCode)
        {

            var adress = await _addressRepository.GetByZipCodeAsync(zipCode);

            if (adress == null)
            {
                _logger.LogWarning("zipcode not found in our database ");
                return null;
            }

            var cepData = await _cepService.GetAddressByZipCodeAsync(zipCode);

            if (cepData == null)
            {

                _logger.LogWarning("zipCode invalid, address not found ");
                return null;
            }


            return new ResponseAddressDto(

              Street: cepData.Logradouro,
              Neighborhood: cepData.Bairro,
              City: cepData.Localidade,
              State: cepData.Uf,
              AreaCode: cepData.Ddd,
              CreatedAt: adress.CreatedAt,
              UpdatedAt: adress.UpdatedAt
            );
        }
    }
}
