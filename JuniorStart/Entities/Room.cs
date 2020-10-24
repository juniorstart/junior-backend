using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorStart.Entities
{
    public class Room
    {
        public Room()
        {
            Messages = new List<Message>();
            Users = new List<User>();
        }

        [Column("RoomId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
