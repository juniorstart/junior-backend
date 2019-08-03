using System.Collections.Generic;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Repository;
using JuniorStart.Services.Interfaces;

namespace JuniorStart.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ApplicationContext _context;
        private readonly IModelFactory<TodoListDto, TodoList> _todoListModelFactory;
        private readonly IModelFactory<TaskDto, Task> _taskModelFactory;

        public TodoListService(ApplicationContext context,IModelFactory<TodoListDto, TodoList> todoListModelFactory,IModelFactory<TaskDto, Task> taskModelFactory)
        {
            _context = context;
            _todoListModelFactory = todoListModelFactory;
            _taskModelFactory = taskModelFactory;
        }
        
        public TaskDto GetTaskById(int id)
        {
            Task task = _context.Tasks.FirstOrDefault(rec => rec.Id.Equals(id));
            return _taskModelFactory.Create(task);
        }

        public List<TodoListDto> GetTodoListsForUser(int ownerId)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateTodoList(TodoListDto requestModel)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateTask(TaskDto requestModel)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateTask(int id, TaskDto requestModel)
        {
            throw new System.NotImplementedException();
        }

        public bool ArchiveTask(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool ArchiveTodoLust(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool ChangeStatusOfsTask(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}