using System;
using System.Linq;
using System.Threading.Tasks;
using JuniorStart.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace JuniorStart.Repository
{
    public class ApplicationSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                
                var db = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                // db.Database.Migrate();

                if (!db.Rooms.Any())
                {
                    db.Rooms.Add(new Room { Name = "FFA" });
                    db.Rooms.Add(new Room { Name = "FFA1" });
                    db.Rooms.Add(new Room { Name = "FFA2" });
                    db.SaveChanges();
                }
                await db.Database.EnsureCreatedAsync();
            }
        }
    }
}