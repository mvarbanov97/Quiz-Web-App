using QuizWebApp.Data.Seeding.Contracts;
using QuizWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Seeding
{
    public class QuestionSeeder : ISeeder
    {
        /// <summary>
        /// Used to seed questions from the Trivia API to the Database.
        /// <para>Should remove it as i can get the data from the API itself</para>
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await LoadQuestionsInDb(dbContext);
        }

        private static async Task LoadQuestionsInDb(ApplicationDbContext db)
        {
            var genreService = new QuestionService(db);
            await genreService.LoadQuestionsInDb();
        }
    }
}
