using FluentValidation;

namespace FrontiersDemo.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.UniversityName).NotEmpty().MaximumLength(500);
        RuleFor(x => x.NumberOfPublications).GreaterThanOrEqualTo(0);
    }
}
