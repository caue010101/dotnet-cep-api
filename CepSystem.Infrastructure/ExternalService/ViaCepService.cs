using System.Text.Json;
using CepSystem.Application.Interfaces;
using CepSystem.Application.Dtos.ExternalDtos;

namespace CepSystem.Infrastructure.ExternalService
{

    public class ViaCepService : IViaCepService
    {

        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<ViaCepResponse?> GetAddressByZipCodeAsync(string zipCode)
        {

            var response = await _httpClient.GetAsync($"{zipCode}/json/");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var data = JsonSerializer.Deserialize<ViaCepResponse>(jsonString, options);

            if (data == null || string.IsNullOrWhiteSpace(data.Logradouro))
            {

                return null;
            }

            return data;
        }

    }
}
