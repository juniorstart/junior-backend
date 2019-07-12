using FluentValidation;
using JuniorStart.DTO;

namespace JuniorStart.Configurations
{
    public class TaskValidator : AbstractValidator<TaskViewModel>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .WithMessage("Description cannot be empty");

            RuleFor(t => t.Id)
                .NotNull()
                .WithMessage("You must assign task to todo list !");
        }
    }
}