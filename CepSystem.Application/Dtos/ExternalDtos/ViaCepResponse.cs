using System.Text.Json.Serialization;


namespace CepSystem.Application.Dtos.ExternalDtos
{
    public record ViaCepResponse
    (
        [property: JsonPropertyName("logradouro")] string? Logradouro,

        [property: JsonPropertyName("bairro")] string? Bairro,

        [property: JsonPropertyName("localidade")] string? Localidade,

        [property: JsonPropertyName("ddd")] string? Ddd,

        [property: JsonPropertyName("uf")] string? Uf
    );
}
