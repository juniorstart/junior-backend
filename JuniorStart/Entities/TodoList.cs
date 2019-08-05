using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using JuniorStart.DTO;

namespace JuniorStart.Entities
{
    public class TodoList : IEntity
    {
        [Column("TodoListId")] public int Id { get; set; }

        public string Name { get; private set; }

        private List<Task> Tasks { get;  set; }
        public bool Status { get; set; }

        private int OwnerId { get;  set; }
        public User Owner { get; private set; }

        public TodoList(TodoListDto listDto)
        {
            Name = listDto.Name;
            Tasks = listDto.Tasks.ConvertAll(x=> new Task(x)).ToList();
            OwnerId = listDto.OwnerId;
        }

        public TodoList()
        {
            
        }
        public void SetName(string name)
        {
            Name = name;
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void SetOwnerId(int id)
        {
            OwnerId = id;
        }

    }
}