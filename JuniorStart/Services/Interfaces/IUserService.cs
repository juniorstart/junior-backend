using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Services.Interfaces
{
    public interface IUserService
    {
        bool Create(UserViewModel user);
        User GetById(int id);
    }
}