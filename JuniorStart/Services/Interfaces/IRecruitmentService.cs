using System.Collections.Generic;
using JuniorStart.DTO;

namespace JuniorStart.Services.Interfaces
{
    public interface IRecruitmentService
    {
        RecruitmentInformationViewModel GetRecruitmentInfoById(int id);
        List<RecruitmentInformationViewModel> GetRecruitmentsForUser(int ownerId);
        bool CreateRecruitmentInfo(RecruitmentInformationViewModel requestModel);
        bool UpdateRecruitmentInfo(int id, RecruitmentInformationViewModel requestModel);
        bool ArchiveRecruitmentInfo(int id);
    }
}