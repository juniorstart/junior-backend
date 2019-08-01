using System;
using System.ComponentModel.DataAnnotations.Schema;
using JuniorStart.DTO;

namespace JuniorStart.Entities
{
    public class RecruitmentInformation : IEntity
    {
        [Column("RecruitmentId")] public int Id { get; set; }

        public int OwnerId { get; set; }

        public string CompanyName { get; private set; }
        public string City { get; private set; }
        public string WorkPlace { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ApplicationDate { get; set; }

        public DateTime DateOfCompanyReply { get; private set; }
        public bool CompanyReply { get; private set; }
        public string Notes { get; private set; }
        public string LinkToApplication { get; private set; }

        public bool IsActive { get; private set; }

        public User Owner { get; set; }
        
        public RecruitmentInformation(){}
        public RecruitmentInformation(RecruitmentInformationDto model)
        {
            CompanyName = model.CompanyName;
            City = model.City;
            WorkPlace = model.WorkPlace;
            DateOfCompanyReply = model.DateOfCompanyReply;
            CompanyReply = model.CompanyReply;
            Notes = model.Notes;
            LinkToApplication = model.LinkToApplication;
            IsActive = true;
            OwnerId = model.OwnerId;
        }

        public void SetIsActive(bool active)
        {
            IsActive = active;
        }
        public void SetCompanyName(string companyName)
        {
            CompanyName = companyName;
        }
        public void SetCity(string city)
        {
            City = city;
        }
        public void SetWorkPlace(string workplace)
        {
            WorkPlace = workplace;
        }

        public void SetNotes(string note)
        {
            Notes = note;
        }

        public void SetLinkToApplication(string link)
        {
            LinkToApplication = link;
        }

        public void SetCompanyReply(bool replied)
        {
            CompanyReply = replied;
        }
    }
}