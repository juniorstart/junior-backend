using System.Net.Http;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Services.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace JuniorStart.Tests
{
    [TestFixture(null)]
    public class Tests
    {
        private readonly IUserService _userService;

        public Tests(IUserService userService)
        {
            _userService = userService;
        }
        
        
        [Test]
        public void CreatingUserTest()
        {
            var user = new UserDto();
            user.Login = "Test login";
            user.Password = "zaq1@WSX";
            user.FirstName = "Jan";
            user.LastName = "Kowalski";
            user.Email = "test@gmail.com";

            bool created = _userService.Create(user);
            if(created)
                Assert.Pass();
            else
            {
                Assert.Fail();
            }
        }
    }
}