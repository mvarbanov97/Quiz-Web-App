using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizWebApp.Data;
using QuizWebApp.Dtos;
using QuizWebApp.Models;
using QuizWebApp.Models.API_Models;
using QuizWebApp.Services.Contracts;
using QuizWebApp.Services.Extensions;
using QuizWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext db;

        public QuizService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<QuizViewModel> GenerateRandomQuizWithDifferentCategories()
        {
            QuizViewModel quiz = new QuizViewModel();

            var randomCategoryIds = await this.GetFiveDifferenetCategoryIds();
            var questions = await this.GetQuestionsFromAPIAsync(randomCategoryIds);

            foreach (var question in questions.Questions)
            {
                this.ReplaceAsciiValuesInQuestion(question);
                quiz.Questions.Add(question);
            }

            return quiz;
        }

        public async Task<List<int>> GetFiveDifferenetCategoryIds()
        {
            var rng = new Random();
            var randomSkip = rng.Next(1, 6);
            var randomCategoryId = await this.db.Categories
                .Skip(randomSkip)
                .Take(5)
                .Distinct()
                .Select(x => x.Id)
                .ToListAsync();

            return randomCategoryId;
        }

        /// <summary>
        /// This method shuffles the answers of the question as the correct answer is added as last afer deserialization of the object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public async Task SubmitQuizToDb(User currentUser, ScoreDto data)
        {
            // Get all user scores with category
            var userScores = db.Score.Where(x => x.UserId == currentUser.Id)
                .Include(x => x.Category)
                .ToList();
            var scoresToUpdate = new List<Score>();

            var categoriesAndBooleans = data.categories.Zip(data.isTrue, (c, b) => new { String = c, Bool = b });
            foreach (var cb in categoriesAndBooleans)
            {
                
                // If the user already answered a question from this category, just update Correct/Incorrect answers count
                if (userScores.Any(x => x.Category.Name == cb.String) && userScores.Count > 0)
                {
                    var scoreToUpdate = this.db.Score.Where(x => x.UserId == currentUser.Id).Include(x => x.Category).Where(x => x.Category.Name == cb.String).FirstOrDefault();
                    var isTrue = cb.Bool;
                    if (isTrue)
                    {
                        scoreToUpdate.CorrectAnswers += 1;
                    }
                    else
                    {
                        scoreToUpdate.WrongAnswers += 1;
                    }

                    scoresToUpdate.Add(scoreToUpdate);
                }
                else
                {
                    // If the user answers a question from an unknown category, add it to the database
                    
                    var scoreToAdd = new Score
                    {
                        User = currentUser,
                        UserId = currentUser.Id,
                        CategoryId = this.db.Categories.Where(x => x.Name == cb.String).Select(x => x.Id).FirstOrDefault(),
                        CorrectAnswers = cb.Bool ? 1 : 0,
                        WrongAnswers = cb.Bool ? 0 : 1,
                    };
                    this.db.Score.Add(scoreToAdd);
                }

            }

            this.db.UpdateRange(scoresToUpdate);
            await this.db.SaveChangesAsync();
        }

        private async Task<QuestionResult> GetQuestionsFromAPIAsync(List<int> listOfCategoryIds)
        {
            QuestionResult questionsObject = new QuestionResult();

            foreach (var categoryId in listOfCategoryIds)
            {
                //Getting 1 questing for each category from the Trivia API
                string url = $"https://opentdb.com/api.php?amount=1&category={categoryId}&type=multiple";

                JObject json = await this.GetJsonStreamFromUrlAsync(url);
                json.Property("response_code").Remove();
                var jsonString = json.ToString();

                var question = JsonConvert.DeserializeObject<QuestionResult>(jsonString);
                var questionToAdd = question.Questions.First();

                // Adding the correct answer to the options list as the API passed it as separate string property 
                // It will become last element of the array but there is shuffle method to shuffle the answers
                // TODO: Make it work better
                questionToAdd.Options.Add(questionToAdd.CorrectAnswer);

                questionsObject.Questions.Add(questionToAdd);
            }

            return questionsObject;
        }

        private void ReplaceAsciiValuesInQuestion(QuestionInfo question)
        {
            if (CheckIfQuestionContainsAsciiValues(question))
            {
                question.Content = question.Content.Replace("&#039;", "'");
                question.Content = question.Content.Replace("&quot;", "\"");
            }
            
            CheckAndReplaceAsciiValuesInTheAnswers(question);
        }

        private static void CheckAndReplaceAsciiValuesInTheAnswers(QuestionInfo question)
        {
            for (int i = 0; i < question.Options.Count; i++)
            {
                if (question.Options[i].Contains("&quot;") || question.Options[i].Contains("&#039;"))
                {
                    question.Options[i] = question.Options[i].Replace("&#039;", "'");
                    question.Options[i] = question.Options[i].Replace("&quot;", "\"");
                }
            }
        }

        private static bool CheckIfQuestionContainsAsciiValues(QuestionInfo question)
        {
            if (question.Content.Contains("&#039;") || question.Content.Contains("&quot;"))
            {
                return true;
            }

            return false;
        }
    }
}
