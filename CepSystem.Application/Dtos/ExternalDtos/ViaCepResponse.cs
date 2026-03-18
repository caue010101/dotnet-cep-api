using System.Text.Json.Serialization;


namespace CepSystem.Application.Dtos.ExternalDtos
{
    public class ViaCepResponse
    {
        [JsonPropertyName("logradouro")]
        public string? Logradouro { get; set; }

        [JsonPropertyName("bairro")]
        public string? Bairro { get; set; }

        [JsonPropertyName("uf")]
        public string? Uf { get; set; }

        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("error")]
        public bool Error { get; set; }
    }
}
