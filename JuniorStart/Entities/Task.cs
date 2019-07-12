using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorStart.Entities
{
    public class Task : IEntity
    {
        [Column("TaskId")]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int TodoListId { get; set; }

        public virtual TodoList TodoList { get; set; }
    }
}