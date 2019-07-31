using System.Collections.Generic;
using JuniorStart.DTO;

namespace JuniorStart.Services.Interfaces
{
    public interface IRecruitmentService
    {
        RecruitmentInformationDto GetRecruitmentInfoById(int id);
        List<RecruitmentInformationDto> GetRecruitmentsForUser(int ownerId);
        bool CreateRecruitmentInfo(RecruitmentInformationDto requestModel);
        bool UpdateRecruitmentInfo(int id, RecruitmentInformationDto requestModel);
        bool ArchiveRecruitmentInfo(int id);
    }
}