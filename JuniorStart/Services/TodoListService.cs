using System;
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
            var todoLists = new List<TodoListDto>();
            var todoList = _context.TodoLists.Where(rec => rec.OwnerId == ownerId);
            foreach (var list in todoList)
            {
                todoLists.Add(_todoListModelFactory.Create(list));
            }

            return todoLists;
        }

        public TodoListDto GetTodoListById(int id)
        {
            TodoList todolist = _context.TodoLists.FirstOrDefault(rec => rec.Id == id);
            return _todoListModelFactory.Create(todolist);
        }

        public bool CreateTodoList(TodoListDto requestModel)
        {
            _context.TodoLists.Add(_todoListModelFactory.Map(requestModel));
            return _context.SaveChanges() > 0;
        }

        public bool CreateTask(TaskDto requestModel)
        {
            _context.Tasks.Add(_taskModelFactory.Map(requestModel));
            return _context.SaveChanges() > 0;
        }
        public bool UpdateTask(int id, TaskDto requestModel)
        {
            Task originalTask = 
                _context.Tasks.FirstOrDefault(model => model.Id.Equals(id));
            Task parsedModel = _taskModelFactory.Map(requestModel);

            if (originalTask is null)
            {
                throw new Exception("Model not found");
            }
            parsedModel.Id = originalTask.Id;
            _context.Entry(originalTask).CurrentValues.SetValues(parsedModel);
            return _context.SaveChanges() > 0;
        }

        public bool ArchiveTask(int id)
        {
            Task taskToArchive = _context.Tasks.FirstOrDefault(rec => rec.Id == id);
            
            if (!(taskToArchive is null))
            {
                taskToArchive.Status = false;
                _context.Tasks.Update(taskToArchive);
            }
            return _context.SaveChanges() > 0;
        }

        public bool ArchiveTodoList(int id)
        {
            TodoList todoListToArchieve = _context.TodoLists.FirstOrDefault(rec => rec.Id == id);
            if (!(todoListToArchieve is null))
            {
                todoListToArchieve.Status = false;
                _context.TodoLists.Update(todoListToArchieve);
            }

            return _context.SaveChanges() > 0;
        }
    }
}