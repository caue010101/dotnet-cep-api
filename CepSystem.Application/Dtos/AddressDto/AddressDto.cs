
namespace CepSystem.Application.Dtos
{

    public record AddressDto(
       string ZipCode,
       string Street,
       string Neighborhood,
       string City,
       string State,
       string AreaCode,
       DateTime CreatedAt,
       DateTime UpdatedAt
    );
}
