using FluentValidation;
using JuniorStart.DTO;

namespace JuniorStart.Configurations
{
    public class RecruitmentInformationValidator : AbstractValidator<RecruitmentInformationViewModel>
    {
        public RecruitmentInformationValidator()
        {
            RuleFor(ri => ri.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("You must provide city name!");

            RuleFor(ri => ri.CompanyName)
                .NotNull()
                .NotEmpty()
                .WithMessage("You must provide company name!");

            RuleFor(ri => ri.WorkPlace)
                .NotNull()
                .NotEmpty().WithMessage("You must provide work place!");

            RuleFor(ri => ri.OwnerId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty().WithMessage("You must assign this!");
        }
    }
}