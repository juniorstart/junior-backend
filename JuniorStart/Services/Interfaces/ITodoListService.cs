using System.Collections.Generic;
using JuniorStart.DTO;

namespace JuniorStart.Services.Interfaces
{
    public interface ITodoListService
    {
        TaskDto GetTaskById(int id);
        List<TodoListDto> GetTodoListsForUser(int ownerId);
        bool CreateTodoList(TodoListDto requestModel);
        bool CreateTask(TaskDto requestModel);
        bool UpdateTask(int id, TaskDto requestModel);
        bool ArchiveTask(int id);
        bool ArchiveTodoLust(int id);
        bool ChangeStatusOfsTask(int id);
    }
}