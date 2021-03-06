using System;
using System.Collections.Generic;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Repository;
using JuniorStart.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
            var todoListEntities = _context.TodoLists.Where(x => x.OwnerId == ownerId).Include(y => y.Tasks).ToList();
          
            var todoLists = new List<TodoListDto>();
            foreach (var list in todoListEntities)
            {
                todoLists.Add(_todoListModelFactory.Create(list));
            }

            return todoLists;
        }

        public int CountTodoListsForUser(int userId)
        {
            return GetTodoListsForUser(userId).Count;
        }

        public int CountNotFinishedTasksForUser(int userId)
        {
            var lists = GetTodoListsForUser(userId);
            int count = 0;
            foreach(var list in lists)
            {
                foreach(var task in list.Tasks)
                {
                    if (!task.Status)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int CountFinishedTasksForUser(int userId)
        {
            var lists = GetTodoListsForUser(userId);
            int count = 0;
            foreach (var list in lists)
            {
                foreach (var task in list.Tasks)
                {
                    if (task.Status)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int CountAllTasksForUser(int userId)
        {
            var lists = GetTodoListsForUser(userId);
            int count = 0;
            foreach (var list in lists)
            {
                foreach (var task in list.Tasks)
                {
                    count++;
                }
            }
            return count;
        }

        public TodoListDto GetTodoListById(int id)
        {
            TodoList todolist = _context.TodoLists.FirstOrDefault(rec => rec.Id == id);
            return _todoListModelFactory.Create(todolist);
        }

        public bool CreateTodoList(TodoListDto requestModel)
        {
            TodoList newModel = _todoListModelFactory.Map(requestModel);
            newModel.SetOwner(_context.Users.SingleOrDefault(x => x.Id == requestModel.OwnerId));
            _context.TodoLists.Add(newModel);
            return _context.SaveChanges() > 0;
        }

        public bool CreateTask(TaskDto requestModel)
        {
            var task = _taskModelFactory.Map(requestModel);
            task.TodoList = _context.TodoLists.FirstOrDefault(x => x.Id == requestModel.TodoListId);
             _context.Tasks.Add(task);
            var sss = _context.Tasks.ToList();
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
                _context.Tasks.Remove(taskToArchive);
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