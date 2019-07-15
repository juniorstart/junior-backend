using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JuniorStart.Entities;
using JuniorStart.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JuniorStart.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration configuration;
        public AuthenticationService(ApplicationContext context)
        {
            _context = context;
        }
        
        public string Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            User user = _context.Users.FirstOrDefault(x => x.Login == username);
            
            if (user == null)
                throw new Exception("Username does not exist");

            if (!VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt))
                throw new Exception("Invalid Password");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(configuration.GetSection("JWT").GetSection("SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenToReturn = tokenHandler.WriteToken(token);
            
            return tokenToReturn;
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 32) return false;
            if (storedSalt.Length != 64) return false;

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