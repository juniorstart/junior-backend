using System.Collections.Generic;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Factories
{
    public class ModelFactory : IModelFactory
    {
        public RecruitmentInformationViewModel Create(RecruitmentInformation recruitmentInformation)
        {
            return new RecruitmentInformationViewModel
            {
                Id = recruitmentInformation.Id,
                City = recruitmentInformation.City,
                Notes = recruitmentInformation.Notes,
                CompanyName = recruitmentInformation.CompanyName,
                OwnerId = recruitmentInformation.Owner.Id,
                WorkPlace = recruitmentInformation.WorkPlace,
                LinkToApplication = recruitmentInformation.LinkToApplication,
                CompanyReply = recruitmentInformation.CompanyReply,
                DateOfCompanyReply = recruitmentInformation.DateOfCompanyReply
            };
        }

        public TaskViewModel Create(Task task)
        {
            return new TaskViewModel
            {
                Id = task.Id,
                Description = task.Description,
                Status = task.Status,
                TodoListId = task.TodoList.Id
            };
        }

        public TodoListViewModel Create(TodoList todoList)
        {
            List<TaskViewModel> tasks = todoList.Tasks.Select(Create).ToList();
            return new TodoListViewModel
            {
                Id = todoList.Id,
                Name = todoList.Name,
                Tasks = tasks
            };
        }

        public UserViewModel Create(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public RecruitmentInformation Map(RecruitmentInformationViewModel recruitmentInformation)
        {
            return new RecruitmentInformation
            {
                City = recruitmentInformation.City,
                Notes = recruitmentInformation.Notes,
                OwnerId = recruitmentInformation.OwnerId,
                CompanyName = recruitmentInformation.CompanyName,
                CompanyReply = recruitmentInformation.CompanyReply,
                WorkPlace = recruitmentInformation.WorkPlace,
                LinkToApplication = recruitmentInformation.LinkToApplication,
                DateOfCompanyReply = recruitmentInformation.DateOfCompanyReply
            };
        }
    }
}