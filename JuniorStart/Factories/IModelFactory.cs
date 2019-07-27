using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Factories
{
    public interface IModelFactory
    {
        RecruitmentInformationViewModel Create(RecruitmentInformation recruitmentInformation);
        TaskViewModel Create(Task task);
        TodoListViewModel Create(TodoList todoList);
        UserViewModel Create(User user);
        RecruitmentInformation Map(RecruitmentInformationViewModel recruitmentInformation);
        User Map(UserViewModel userViewModel);
    }
}