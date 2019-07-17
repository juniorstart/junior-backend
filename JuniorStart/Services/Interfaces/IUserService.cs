using JuniorStart.Entities;

namespace JuniorStart.Services.Interfaces
{
    public interface IUserService
    {
        bool Create(User user);
        User GetById(int id);
    }
}