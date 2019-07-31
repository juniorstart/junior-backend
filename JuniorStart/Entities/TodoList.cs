using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorStart.Entities
{
    public class TodoList : IEntity
    {
        [Column("TodoListId")] public int Id { get; set; }

        public string Name { get; set; }

        public List<Task> Tasks { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}