using FluentValidation;

namespace Identity.Identity.Features.Register.Handler;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email address provided.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Must(IsValidPassword)
            .WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit.");

        RuleFor(x => x.PasswordConfirm)
            .NotEmpty().WithMessage("Confirm password cannot be empty.")
            .Equal(x => x.Password).WithMessage("Confirm password must match the password.");
    }


    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        return password.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit);
    }
}