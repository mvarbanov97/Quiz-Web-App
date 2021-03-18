using QuizWebApp.Data.Seeding.Contracts;
using QuizWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Seeding
{
    public class RoleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            dbContext.Roles.Add(new Role
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
            });
            dbContext.SaveChanges();

            dbContext.Roles.Add(new Role
            {
                Name = "UserManager",
                NormalizedName = "USERMANAGER"
            });
            dbContext.SaveChanges();

            dbContext.Roles.Add(new Role
            {
                Name = "User",
                NormalizedName = "USER"
            });
           dbContext.SaveChanges();
        }
    }
}
