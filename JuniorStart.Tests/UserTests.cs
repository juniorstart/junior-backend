using System;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Repository;
using JuniorStart.Services;
using JuniorStart.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace JuniorStart.Tests
{
    [TestFixture()]
    public class Tests
    {
        private DbContextOptions<ApplicationContext> options;
        private Mock<IModelFactory<UserDto, User>> factoryMock;

        [SetUp]
        public void Init()
        {
            options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AppDatabase")
                .Options;
        }
        [Test]
        public void CreatingUserTest()
        {

            var userDto = new UserDto
            {
                Login = "Test login",
                Password = "zaq1@WSX",
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "test@gmail.com"
            };

            factoryMock = new Mock<IModelFactory<UserDto, User>>();
            factoryMock.Setup(x => x.Map(It.IsAny<UserDto>())).Returns(new User(userDto));

            using (var context = new ApplicationContext(options))
            {
                IUserService userService = new UserService(context, factoryMock.Object);

                //Act
                bool created = userService.Create(userDto);

                //Assert
                Assert.That(created == true);
                Assert.That(context.Users.Count() == 1);
                Assert.That(context.Users.First().Login.Equals("Test login"));
                Assert.That(context.Users.First().FirstName.Equals("Jan"));
                Assert.That(context.Users.First().LastName.Equals("Kowalski"));
                factoryMock.Verify(mock => mock.Map(It.IsAny<UserDto>()), Times.Once);
                //itd
            }
        }

        [Test]
        public void LoginUserTest()
        {
            var config = new Mock<IConfiguration>();
            using (var context = new ApplicationContext(options))
            {
                var authService = new AuthenticationService(context,config.Object);

                var configurationSection = new Mock<IConfigurationSection>();
                configurationSection.Setup(a => a.Value).Returns("YOUR VERY LONG SECRET KEY");
              
                config.Setup(a => a.GetSection("JWT").GetSection("SecretKey")).Returns(configurationSection.Object);
                
                string jwt = authService.Authenticate("Test login", "zaq1@WSX");
                Assert.That(!string.IsNullOrEmpty(jwt));

            }
        }
    }
}
