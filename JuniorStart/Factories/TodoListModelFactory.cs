using System.Collections.Generic;
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
                OwnerId = model.Owner.Id,
                Status = true,
                Tasks = new List<TaskDto>()
            };
        }

        public TodoList Map(TodoListDto model)
        {

            return new TodoList
            {
                Id = model.Id,
                Name = model.Name,
                OwnerId = model.OwnerId,
                Status = true,
                Tasks = new List<Task>()
            };

        }
    }
}