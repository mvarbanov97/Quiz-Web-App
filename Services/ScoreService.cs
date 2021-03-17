using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Data;
using QuizWebApp.Models;
using QuizWebApp.Services.Contracts;
using QuizWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services
{
    public class ScoreService : IScoreService
    {
        //TODO: Add Automapper so I can map Domain Models with Dtos, Service Models, Api Objects
        private readonly ApplicationDbContext db;

        /// <summary>
        /// A constructor that creates a new instance of ScoreService
        /// </summary>
        /// <param name="db">A ApplicationDbContext dependency injected into the constructor..</param>
        public ScoreService(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// An async method which returns a Score from the database and create a viewModel to return
        /// </summary>
        /// <param name="user">Passing the User in order to get his Id. Could use more of its properties in the future</param>
        /// <param name="categoryId">The Id of the Category to return.</param>
        /// <returns>
        /// A Task<ScoreViewModel> The Score with Id equal to categoryId parameter.
        /// </returns>
        public async Task<ScoreViewModel> GetScoreByCategoryId(User user, int categoryId)
        {
            var score = this.db.Score
                .Where(s => s.User.Id == user.Id)
                .Where(s => s.CategoryId == categoryId)
                .FirstOrDefault();

            var viewModel = new ScoreViewModel
            {
                CorrectAnswers = score.CorrectAnswers,
                WrongAnswers = score.WrongAnswers,
            };

            return viewModel;
        }

        /// <summary>
        /// An async method which returns all of the user answered questions
        /// </summary>
        /// <param name="user">Passing the User in order to get his Id. Could use more of its properties in the future</param>
        /// <returns>
        /// A Task<ScoreViewModel> The sum of all correct and incorrect questions with Id equal to user parameter.
        /// </returns>
        public async Task<ScoreViewModel> GetTotalScoreCount(User user)
        {
            var allCorrectAnswersCount = this.db.Score
                .Where(x => x.UserId == user.Id)
                .Select(x => x.CorrectAnswers)
                .Sum();

            var allIncorrectAnswersCount = this.db.Score
                .Where(x => x.UserId == user.Id)
                .Select(x => x.WrongAnswers)
                .Sum();

            var viewModel = new ScoreViewModel
            {
                CorrectAnswers = allCorrectAnswersCount,
                WrongAnswers = allIncorrectAnswersCount,
            };

            return viewModel;
        }
    }
}
