using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Factories
{
    public class TaskModelFactory : IModelFactory<TaskDto, Task>
    {
        public TaskDto Create(Task model)
        {
            return new TaskDto
            {
                Description = model.Description,
                Id = model.Id,
                Status = model.Status,
                TodoListId = model.TodoListId
            };
        }

        public Task Map(TaskDto model)
        {
            return new Task(model);
        }
    }
}