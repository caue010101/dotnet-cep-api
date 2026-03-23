using CepSystem.Application.Dtos;
using CepSystem.Application.Interfaces;
using CepSystem.Domain.Interfaces;
using CepSystem.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace CepSystem.Application.Services
{

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;
        private readonly IViaCepService _cepService;
        private readonly IUnitOfWork _unitOfWork;
        public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger, IViaCepService cepService, IUnitOfWork unitOfWork)
        {
            this._addressRepository = addressRepository;
            this._logger = logger;
            this._cepService = cepService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<ResponseAddressDto?> GetByZipCodeAsync(string zipCode)
        {

            var address = await _addressRepository.GetByZipCodeAsync(zipCode);


            if (address != null)
            {

                return new ResponseAddressDto(
                  Street: address.Street,
                  Neighborhood: address.Neighborhood,
                  City: address.City,
                  State: address.State,
                  AreaCode: address.AreaCode,
                  CreatedAt: address.CreatedAt,
                  UpdatedAt: address.UpdatedAt

                );
            }

            _logger.LogWarning("Adress {ZipCode} not found in database ", zipCode);


            var cepData = await _cepService.GetAddressByZipCodeAsync(zipCode);

            if (cepData == null)
            {

                _logger.LogError("ZipCode {ZipCode} invalid or not found in ViaCep ", zipCode);
                return null;
            }

            try
            {
                var newAddres = new Address(
                  zipCode,
                  cepData.Logradouro,
                  cepData.Bairro,
                  cepData.Localidade,
                  cepData.Uf,
                  cepData.Ddd
                );

                _unitOfWork.BeginTransaction();

                await _addressRepository.AddAsync(newAddres);

                _unitOfWork.Commit();

                _logger.LogInformation("Address {zipCode} persisted successfully", zipCode);
            }
            catch (Exception e)

            {
                _unitOfWork.Rollback();
                _logger.LogError(e, "Failed persist {ZipCode} in database", zipCode);
            }

            return new ResponseAddressDto(

              Street: cepData.Logradouro,
              Neighborhood: cepData.Bairro,
              City: cepData.Localidade,
              State: cepData.Uf,
              AreaCode: cepData.Ddd,
              CreatedAt: DateTime.UtcNow,
              UpdatedAt: DateTime.UtcNow
            );
        }
    }
}
