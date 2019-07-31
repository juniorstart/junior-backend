using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JuniorStart.DTO;

namespace JuniorStart.Entities
{
    public class User : IEntity
    {
        [Column("UserId")] 
        public int Id { get; set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }
        
        public string Login { get; private set; }

        public string Password { get; private set; }

        private bool IsActive { get;  set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        private List<RecruitmentInformation> RecruitmentInformations { get; set; }

        private List<TodoList> TodoLists { get; set; }
        public User(UserDto model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Login = model.Login;
            Password = model.Password;
            IsActive = true;
        }
        public void SetSalt(byte[] salt)
        {
            PasswordSalt = salt;
        }
        public void SetHash(byte[] hash)
        {
            PasswordHash = hash;
        }
        public void SetTodoLists(List<TodoList> list)
        {
            var firstNotSecond = list.Where(i => !TodoLists.Contains(i)).ToList();
            var secondNotFirst = TodoLists.Where(i => !list.Contains(i)).ToList();
            if (!firstNotSecond.Any() && !secondNotFirst.Any())
            {
                TodoLists = list;
            }
        }
        
        public void SetIRecruitmentInformations(List<RecruitmentInformation> list)
        {
            var firstNotSecond = list.Where(i => !RecruitmentInformations.Contains(i)).ToList();
            var secondNotFirst = RecruitmentInformations.Where(i => !list.Contains(i)).ToList();
            if (!firstNotSecond.Any() && !secondNotFirst.Any())
            {
                RecruitmentInformations = list;
            }
        }
        public void SetIsActive(bool isActive)
        {
            if (IsActive == isActive)
            {
                return;
            }

            IsActive = isActive;
        }
        public void SetPassword(string password)
        {
            if (Password == password)
            {
                return;
            }

            Password = password;
        }
        public void SetLogin(string login)
        {
            if (Login == login)
            {
                return;
            }

            Login = login;
        }
        
        public void SetEmail(string email)
        {
            if (Email == email)
            {
                return;
            }

            Email = email;
        }
        public void SetLastName(string lastName)
        {
            if (LastName == lastName)
            {
                return;
            }

            LastName = lastName;
        }
        public void SetFirstName(string firstName)
        {
            if (FirstName == firstName)
            {
                return;
            }

            FirstName = firstName;
        }
    }
}