using FluentValidation;
using JuniorStart.DTO;
using JuniorStart.ViewModels;

namespace JuniorStart.Configurations
{
    public class LoginModelValidator : AbstractValidator<LoginRequest>
    {
        public LoginModelValidator()
        {
            RuleFor(u => u.Login)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .Matches("^[a-zA-Z].*")
                .WithMessage("Login must be between 4 and 30 letters, and starts with letter.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).*")
                .WithMessage("Password not match the rules!");
        }
    }
}