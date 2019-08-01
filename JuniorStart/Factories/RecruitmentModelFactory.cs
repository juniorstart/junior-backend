using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Factories
{
    public class RecruitmentModelFactory : IModelFactory<RecruitmentInformationDto, RecruitmentInformation>
    {
        public RecruitmentInformationDto Create(RecruitmentInformation model)
        {
            return new RecruitmentInformationDto
            {
                Id = model.Id,
                City = model.City,
                Notes = model.Notes,
                CompanyName = model.CompanyName,
                OwnerId = model.Owner.Id,
                WorkPlace = model.WorkPlace,
                LinkToApplication = model.LinkToApplication,
                CompanyReply = model.CompanyReply,
                DateOfCompanyReply = model.DateOfCompanyReply,
                ApplicationDate = model.ApplicationDate
            };
        }

        public RecruitmentInformation Map(RecruitmentInformationDto model)
        {
            return new RecruitmentInformation(model);
        }
    }
}