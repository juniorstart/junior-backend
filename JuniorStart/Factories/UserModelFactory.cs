using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Factories
{
    public class UserModelFactory : IModelFactory<UserDto, User>
    {
        public UserDto Create(User model)
        {
            return new UserDto
            {
                Id = model.Id,
                Email = model.Email,
                Login = model.Login,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public User Map(UserDto model)
        {
            return new User
            {
                Email = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Password = model.Password,
                IsActive = true
            };
        }
    }
}