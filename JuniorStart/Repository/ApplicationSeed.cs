using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace JuniorStart.Repository
{
    public class ApplicationSeed
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                await db.Database.EnsureCreatedAsync();
            }
        }
    }
}