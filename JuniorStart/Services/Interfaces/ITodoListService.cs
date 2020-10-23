using System.Collections.Generic;
using JuniorStart.DTO;

namespace JuniorStart.Services.Interfaces
{
    public interface ITodoListService
    {
        TaskDto GetTaskById(int id);
        List<TodoListDto> GetTodoListsForUser(int ownerId);
        TodoListDto GetTodoListById(int id);
        bool CreateTodoList(TodoListDto requestModel);
        bool CreateTask(TaskDto requestModel);
        bool UpdateTask(int id, TaskDto requestModel);
        bool ArchiveTask(int id);
        bool ArchiveTodoList(int id);
        int CountTodoListsForUser(int userId);
        int CountFinishedTasksForUser(int userId);
        int CountNotFinishedTasksForUser(int userId);
        int CountAllTasksForUser(int userId);

    }
}