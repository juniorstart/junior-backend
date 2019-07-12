using System;
using JuniorStart.Entities;

namespace JuniorStart.DTO
{
    public class RecruitmentInformationViewModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string City { get; set; }
        public string WorkPlace { get; set; }

        public DateTime DateOfCompanyReply { get; set; }
        public bool CompanyReply { get; set; }
        public string Notes { get; set; }
        public string LinkToApplication { get; set; }

        public int OwnerId { get; set; }
    }
}