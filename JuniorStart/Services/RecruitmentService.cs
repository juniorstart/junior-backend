using System;
using System.Collections.Generic;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Repository;
using JuniorStart.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JuniorStart.Services
{
    public class RecruitmentService : IRecruitmentService
    {
        private readonly ApplicationContext _context;
        private readonly IModelFactory<RecruitmentInformationDto, RecruitmentInformation> _modelFactory;

        public RecruitmentService(ApplicationContext context,
            IModelFactory<RecruitmentInformationDto, RecruitmentInformation> modelFactory)
        {
            _context = context;
            _modelFactory = modelFactory;
        }

        public RecruitmentInformationDto GetRecruitmentInfoById(int id)
        {
            RecruitmentInformation recruitmentInformation =
                _context.RecruitmentInformations.FirstOrDefault(rec => rec.Id.Equals(id));
            return _modelFactory.Create(recruitmentInformation);
        }

        public List<RecruitmentInformationDto> GetRecruitmentsForUser(int ownerId)
        {
            List<RecruitmentInformationDto> recruitments = _context.RecruitmentInformations
                .Where(rec => rec.OwnerId.Equals(ownerId) && rec.IsActive)
                .Select(rec => _modelFactory.Create(rec)).ToList();
            return recruitments;
        }

        public bool CreateRecruitmentInfo(RecruitmentInformationDto requestModel)
        {
            RecruitmentInformation newModel = _modelFactory.Map(requestModel);
            newModel.Owner = _context.Users.SingleOrDefault(x => x.Id == requestModel.OwnerId);
            
            _context.RecruitmentInformations.Add(newModel);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateRecruitmentInfo(int id, RecruitmentInformationDto requestModel)
        {
            RecruitmentInformation originalModel =
                _context.RecruitmentInformations.FirstOrDefault(model => model.Id.Equals(id));
            RecruitmentInformation parsedModel = _modelFactory.Map(requestModel);

            if (originalModel is null)
            {
                throw new Exception("Model not found");
            }

            parsedModel.Id = originalModel.Id;

            _context.Entry(originalModel).CurrentValues.SetValues(parsedModel);
            return _context.SaveChanges() > 0;
        }

        public bool ArchiveRecruitmentInfo(int id)
        {
            RecruitmentInformation modelToArchive =
                _context.RecruitmentInformations.FirstOrDefault(model => model.Id == id);

            if (modelToArchive != null)
            {
                modelToArchive.SetIsActive(false);
                _context.RecruitmentInformations.Update(modelToArchive);
            }

            return _context.SaveChanges() > 0;
        }
    }
}