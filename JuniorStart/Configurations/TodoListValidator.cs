using FluentValidation;
using JuniorStart.DTO;
using JuniorStart.ViewModels;

namespace JuniorStart.Configurations
{
    public class TodoListValidator : AbstractValidator<TodoListDto>
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