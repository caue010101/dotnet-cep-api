
namespace CepSystem.Application.Dtos
{

    public record ResponseAddressDto(
      string Street,
      string Neighborhood,
      string City,
      string State,
      string AreaCode,
      DateTime CreatedAt,
      DateTime UpdatedAt
    );
}
