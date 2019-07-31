using System.Collections.Generic;

namespace JuniorStart.DTO
{
    public class TodoListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TaskDto> Tasks { get; set; }

        public int OwnerId { get; set; }
    }
}