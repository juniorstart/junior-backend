using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorStart.Entities
{
    public class Room : IEntity
    {
        public Room()
        {
            Messages = new List<Message>();
            Users = new List<User>();
        }

        [Column("RoomId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
    }
}
