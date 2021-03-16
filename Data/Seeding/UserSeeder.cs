using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QuizWebApp.Data.Seeding.Contracts;
using QuizWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Seeding
{
    public class UserSeeder : ISeeder
    {
        /// <summary>
        /// Creates an admin user so the person who will check the app can log in and try the application.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var hasher = new PasswordHasher<User>();

            dbContext.Users.Add(new User
            {
                Name = "Test User"
                UserName = "testUsername",
                NormalizedUserName = "TESTUSERNAME",
                Email = "test@abv.bg",
                NormalizedEmail = "TEST@ABV.BG",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = String.Concat(Array.ConvertAll(Guid.NewGuid().ToByteArray(), b => b.ToString("X2"))),
            });
            dbContext.SaveChanges();

            var testUserJustCreated = dbContext.Users.FirstOrDefault(x => x.Email == "test@abv.bg");
            testUserJustCreated.PasswordHash = hasher.HashPassword(testUserJustCreated, "123456");
            dbContext.SaveChanges();

            dbContext.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>
            {
                RoleId = dbContext.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                UserId = testUserJustCreated.Id,
            });
           dbContext.SaveChanges();
        }
    }
}
