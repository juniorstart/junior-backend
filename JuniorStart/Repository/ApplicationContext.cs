using JuniorStart.Models;
using Microsoft.EntityFrameworkCore;

namespace JuniorStart.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}