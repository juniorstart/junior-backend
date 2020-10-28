using FluentValidation;
using FluentValidation.Validators;
using JuniorStart.DTO;
using JuniorStart.ViewModels;

namespace JuniorStart.Configurations
{
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        public UserValidator()
        {
            RuleFor(u => u.User.FirstName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("First name cannot be empty and maximum length of 30 letters.");

            RuleFor(u => u.User.LastName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Last name cannot be empty and maximum length of 30 letters.");


            RuleFor(u => u.User.Login)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .Matches("^[a-zA-Z].*")
                .WithMessage("Login must be between 4 and 30 letters, and starts with letter.");

            RuleFor(u => u.User.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).*")
                .WithMessage("Password not match the rules!");

            RuleFor(u => u.User.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .WithMessage("Email not match the rules!");
        }
    }
}