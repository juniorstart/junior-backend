using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorStart.Entities
{
    public class Message : IEntity
    {
        [Column("MessageId")]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public DateTime SendTime { get; set; }
        public Message() => SendTime = DateTime.Now;
    }
}
