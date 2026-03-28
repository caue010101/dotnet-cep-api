
namespace CepSystem.Application.Dtos
{

    public record CreateUserDto(
      Guid Id,
      string Name,
      string Email,
      string Password
    );
}
