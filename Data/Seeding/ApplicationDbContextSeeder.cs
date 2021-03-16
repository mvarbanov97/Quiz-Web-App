using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using QuizWebApp.Data.Seeding.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Seeding
{
    public class ApplicationDbContextSeeder : ISeeder
    {
        /// <summary>
        /// An async method that seeds the Database if its empty and log message after each seeder. Included in the pipeline
        /// </summary>
        /// <param name="dbContext">Passed so the seed data can be saved to the Database</param>
        /// <param name="serviceProvider">Used for getting the ILogger service</param>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<ISeeder>
                          {
                              new RoleSeeder(),
                              new UserSeeder(),
                              new CategorySeeder(),
                              new QuestionSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
