using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizWebApp.Data;
using QuizWebApp.Models;
using QuizWebApp.Models.API_Models;
using QuizWebApp.Services.Contracts;
using QuizWebApp.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext db;

        public QuestionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task LoadQuestionsInDb()
        {
            if (this.DatabaseContainsQuestions())
                return;

            var allCategoryIds = this.db.Categories.Select(x => x.Id).ToList();
            var allCategoryNames = this.db.Categories.Select(x => x.Name).ToList();

            foreach (var id in allCategoryIds)
            {
                //var apiObject = await this.GetQuestionsFromAPIAsync(id);
                //var questions = apiObject;

                //foreach (var question in questions)
                //{
                //    // Replacing single and double quotes from the question content as the API returns them as ASCII value
                //    question.Content = question.Content.Replace("&#039;", "'");
                //    question.Content = question.Content.Replace("&quot;", "\"");
                //    question.CategoryId = id;
                //}

                //await this.db.Questions.AddRangeAsync(questions);
                //await this.db.SaveChangesAsync();
            }
        }

        public async Task<QuestionResult> GetQuestionsFromAPIAsync(int categoryId)
        {
            //Getting 50 questing for each category from the Trivia API
            string url = $"https://opentdb.com/api.php?amount=50&category={categoryId}&type=multiple";

            JObject json = await this.GetJsonStreamFromUrlAsync(url);

            var questions = JsonConvert.DeserializeObject<QuestionResult>(json.ToString());

            return questions;
        }

        private bool DatabaseContainsQuestions()
        {
            if (this.db.Questions == null || this.db.Questions.Count() == 0)
                return false;

            return true;
        }
    }
}
