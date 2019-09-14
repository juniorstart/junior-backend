using System.Collections.Generic;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Factories
{
    public class TodoListModelFactory : IModelFactory<TodoListDto, TodoList>
    {
        public TodoListDto Create(TodoList model)
        {
            return new TodoListDto
            {
                Id = model.Id,
                Name = model.Name,
                OwnerId = model.OwnerId,
                Status = true,
                Tasks = model.Tasks.ConvertAll(CreateTaskDto).ToList()
            };
        }

        public TodoList Map(TodoListDto model)
        {
            var todoList = new TodoList(model);
            
            return todoList;
        }

        public TaskDto CreateTaskDto(Task task)
        {
            return new TaskDto()
            {
                Id =  task.Id,
                Description =  task.Description,
                Status =  task.Status,
                TodoListId = task.TodoListId
            };
        }
    }
}