using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Repository;
using JuniorStart.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Linq;

namespace JuniorStart.Tests
{
    [TestFixture()]
    public class RecruitmentTest
    {
        private Mock<IModelFactory<UserDto, User>> factoryMock;
        private DbContextOptions<ApplicationContext> options;

        [SetUp]
        public void Init()
        {
            options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AppDatabase")
                .Options;
        }

        [Test]
        public void GetRecruitmentTest()
        {
            var config = new Mock<IModelFactory<RecruitmentInformationDto, RecruitmentInformation>>();

            var recruitmentInformationDto = new RecruitmentInformationDto
            {
                ApplicationDate = new System.DateTime(),
                City = "Kraków",
                CompanyName = "TestCompany",
                WorkPlace = "Fullstack",
                DateOfCompanyReply = new System.DateTime(),
                OwnerId = 3,
                
            };

            using (var context = new ApplicationContext(options))
            {
                context.RecruitmentInformations.Add(new RecruitmentInformation(recruitmentInformationDto));
                context.SaveChanges();
                var recruitmentService = new RecruitmentService(context, config.Object);

                var recruitmentsList = recruitmentService.GetRecruitmentsForUser(3);

                Assert.That(recruitmentsList.Count > 0);
                Assert.That(recruitmentsList.FirstOrDefault().OwnerId == 3);
                Assert.That(recruitmentsList.FirstOrDefault().City.Equals("Kraków"));

            }
        }
    }
}
