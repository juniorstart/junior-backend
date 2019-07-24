using System.Collections.Generic;

namespace JuniorStart.DTO
{
    public class TodoListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TaskViewModel> Tasks { get; set; }

        public int OwnerId { get; set; }
    }
}