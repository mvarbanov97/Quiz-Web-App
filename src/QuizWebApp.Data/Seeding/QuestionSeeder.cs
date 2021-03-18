using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizWebApp.Data.Seeding.Contracts;
using QuizWebApp.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            await this.LoadQuestionsInDb(dbContext);
        }

        public async Task<QuestionResult> GetQuestionsFromAPIAsync(int categoryId)
        {
            //Getting 50 questing for each category from the Trivia API
            string url = $"https://opentdb.com/api.php?amount=50&category={categoryId}&type=multiple";

            JObject json = await this.GetJsonStreamFromUrlAsync(url);

            var questions = JsonConvert.DeserializeObject<QuestionResult>(json.ToString());

            return questions;
        }

        public async Task<JObject> GetJsonStreamFromUrlAsync(string url)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(result);

            return json;
        }

        public async Task LoadQuestionsInDb(ApplicationDbContext db)
        {
            if (this.DatabaseContainsQuestions(db))
                return;

            var allCategoryIds = db.Categories.Select(x => x.Id).ToList();
            var allCategoryNames = db.Categories.Select(x => x.Name).ToList();

            foreach (var id in allCategoryIds)
            {
                var apiObject = await this.GetQuestionsFromAPIAsync(id);
                var questions = apiObject;

                foreach (var question in questions.Questions)
                {
                    // Replacing single and double quotes from the question content as the API returns them as ASCII value
                    question.Content = question.Content.Replace("&#039;", "'");
                    question.Content = question.Content.Replace("&quot;", "\"");
                }

                await db.Questions.AddRangeAsync();
                await db.SaveChangesAsync();
            }
        }

        private bool DatabaseContainsQuestions(ApplicationDbContext db)
        {
            if (db.Questions == null || db.Questions.Count() == 0)
                return false;

            return true;
        }

    }
}
