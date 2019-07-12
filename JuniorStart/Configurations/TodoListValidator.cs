using FluentValidation;
using JuniorStart.DTO;

namespace JuniorStart.Configurations
{
    public class TodoListValidator : AbstractValidator<TodoListViewModel>
    {
        public TodoListValidator()
        {
            RuleFor(tl => tl.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .WithMessage("Name cannot be empty!");
            
            RuleFor(tl => tl.OwnerId)
                .NotEmpty()
                .NotNull()
                .WithMessage("You must assign todo list!");
        }
    }
}