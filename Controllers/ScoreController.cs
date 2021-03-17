using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Common;
using QuizWebApp.Data;
using QuizWebApp.Dtos;
using QuizWebApp.Models;
using QuizWebApp.Services.Contracts;
using QuizWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Controllers
{
    public class ScoreController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly IScoreService scoreService;
        private readonly ICategoryService categoryService;

        public ScoreController(ApplicationDbContext db, UserManager<User> userManager, IScoreService scoreService, ICategoryService categoryService)
        {
            this.db = db;
            this.userManager = userManager;
            this.scoreService = scoreService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoryViewModel();

            this.ViewData["categories"] = SelectListGenerator.GetAllCategories(this.categoryService);
            return this.View(viewModel);
        }

        public async Task<IActionResult> AverageScore()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.scoreService.GetTotalScoreCount(currentUser);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ScoreByCategory(int categoryId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var isUserAnsweredQuestionsFromCategory = this.db.Score
                .Where(x => x.CategoryId == categoryId)
                .Where(x => x.UserId == currentUser.Id)
                .Any();

            var viewModel = new ScoreViewModel();

            if (isUserAnsweredQuestionsFromCategory)
            {
                viewModel = await this.scoreService.GetScoreByCategoryId(currentUser, categoryId);
            }
            else
            {
                // Redirect to Quiz Index Action if the user have not answered a question from category
                return  this.RedirectToAction("Index", "Quiz");
            }

            return this.View(viewModel);
        }

        public IActionResult GetLastScore(int correct, int questCount)
        {
            var viewModel = new ScoreViewModel
            {
                CorrectAnswers = correct,
                WrongAnswers = correct - questCount,
            };

            return this.View(viewModel);
        }
    }
}
