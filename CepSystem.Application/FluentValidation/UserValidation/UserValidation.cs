using FluentValidation;
using CepSystem.Application.Dtos;

namespace CepSystem.Application.FluentValidation
{

    public class UserValidation : AbstractValidator<CreateUserDto>
    {
        public UserValidation()
        {

            RuleFor(x => x.Name).NotEmpty()
              .WithMessage("Name is required")
              .MinimumLength(2).WithMessage("The name must contain at least 2 characters")
              .MaximumLength(50).WithMessage("The name must contain a maximum of 50 characters");

            RuleFor(x => x.Email).NotEmpty()
              .WithMessage("Email is required")
              .EmailAddress().WithMessage("Email invalid ");

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("Password is required ")

              .MinimumLength(8)
              .WithMessage("The password must contain at least 8 characters ")

              .MaximumLength(50)
              .WithMessage("The password must contain a maximum of 50 characters ")

              .Matches("[A-Z]")
              .WithMessage("The password must contain at one uppercase letter ")

              .Matches("[a-z]")
              .WithMessage("The password must contain at one lowercase letter ")

              .Matches("[0-9]")
              .WithMessage("The password must contain at one number ")

              .Matches("[^a-zA-Z0-9]")
              .WithMessage("The password must contain at one special character");
        }
    }
}
