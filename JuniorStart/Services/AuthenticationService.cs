using System;
using System.Linq;
using JuniorStart.Entities;
using JuniorStart.Repository;

namespace JuniorStart.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationContext _context;

        public AuthenticationService(ApplicationContext context)
        {
            _context = context;
        }
        
        public bool Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            User user = _context.Users.FirstOrDefault(x => x.Login == username);
            
            if (user == null)
                throw new Exception("Username does not exist");

            if (!VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt))
                throw new Exception("Invalid Password");

            return true;
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(password);
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", password);
            if (storedHash.Length != 32) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "storedHash");
            if (storedSalt.Length != 64) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "storedHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA256(storedSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        
        
    }
}