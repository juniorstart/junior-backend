using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorStart.Entities
{
    public class RecruitmentInformation : IEntity
    {
        [Column("RecruitmentId")] public int Id { get; set; }

        public int OwnerId { get; set; }

        public string CompanyName { get; set; }
        public string City { get; set; }
        public string WorkPlace { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ApplicationDate { get; set; }

        public DateTime DateOfCompanyReply { get; set; }
        public bool CompanyReply { get; set; }
        public string Notes { get; set; }
        public string LinkToApplication { get; set; }

        public bool IsActive { get; set; }

        public User Owner { get; set; }
    }
}