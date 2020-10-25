using FluentValidation;
using FluentValidation.Validators;
using JuniorStart.DTO;

namespace JuniorStart.Configurations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("First name cannot be empty and maximum length of 30 letters.");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Last name cannot be empty and maximum length of 30 letters.");


            RuleFor(u => u.Login)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .Matches("^[a-zA-Z].*")
                .WithMessage("Login must be between 6 and 30 letters, and starts with letter.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).*")
                .WithMessage("Password not match the rules!");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .WithMessage("Email not match the rules!");
        }
    }
}