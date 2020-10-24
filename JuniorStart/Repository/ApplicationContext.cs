using JuniorStart.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuniorStart.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<RecruitmentInformation> RecruitmentInformations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}