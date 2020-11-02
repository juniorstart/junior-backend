using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JuniorStart.DTO;

namespace JuniorStart.Entities
{
    public class Task : IEntity
    {
        [Column("TaskId")]
        public int Id { get; set; }

        public string Description { get; private set; }

        public bool Status { get;  set; }

        public int TodoListId { get; set; }

        public TodoList TodoList { get; set; }

        public Task(TaskDto taskDto)
        {
            Description = taskDto.Description;
            Status = taskDto.Status;
            TodoListId = taskDto.TodoListId;
        }

        public Task()
        {
            
        }

        public void SetDescription(string desc)
        {
            Description = desc;
        }

        public void SetStatus(bool status)
        {
            Status = status;
        }

        public void SetTodoListId(int id)
        {
            TodoListId = id;
        }

        public void SetTodoList(TodoList list)
        {
            TodoList = list;
        }
        
    }
}