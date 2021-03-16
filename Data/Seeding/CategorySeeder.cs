using Newtonsoft.Json;
using QuizWebApp.Data.Seeding.Contracts;
using QuizWebApp.Models;
using QuizWebApp.Services;
using QuizWebApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Seeding
{
    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await LoadCategoriesInDb(dbContext);
        }

        private static async Task LoadCategoriesInDb(ApplicationDbContext db)
        {
            var genreService = new CategoryService(db);
            await genreService.LoadGenresInDbAsync();
        }
    }
}
