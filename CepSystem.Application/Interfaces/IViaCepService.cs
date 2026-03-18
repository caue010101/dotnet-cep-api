using CepSystem.Application.Dtos.ExternalDtos;


namespace CepSystem.Application.Interfaces
{
    public interface IViaCepService
    {

        Task<ViaCepResponse?> GetAddressByZipCodeAsync(string zipCode);
    }
}
