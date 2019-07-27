using FluentValidation;
using JuniorStart.DTO;

namespace JuniorStart.Configurations
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(l=>l.Login)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Matches("^[a-zA-Z].*")
                .WithMessage("Login must be between 6 and 30 letters, and starts with letter.");
                
            RuleFor(l=>l.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).*")
                .WithMessage("Password not match the rules!");
        }
    }
}