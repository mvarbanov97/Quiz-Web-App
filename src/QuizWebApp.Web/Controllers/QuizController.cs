using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using QuizWebApp.Data;
using QuizWebApp.Common.ViewModels;
using QuizWebApp.Common.Dtos;
using QuizWebApp.Models;
using QuizWebApp.Models.API_Models;
using QuizWebApp.Services;
using QuizWebApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Web.Controllers
{
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IQuizService quizService;

        public QuizController(
            ILogger<QuizController> logger,
            ApplicationDbContext db,
            UserManager<User> userManager,
            IQuizService quizService, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.db = db;
            this.userManager = userManager;
            this.quizService = quizService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.quizService.GenerateRandomQuizWithDifferentCategories();

            foreach (var question in viewModel.Questions)
            {
                this.quizService.Shuffle<string>(question.Options);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuiz([FromBody]ScoreDto data)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.quizService.SubmitQuizToDb(currentUser, data);
            int correctAnswers = data.isTrue.Where(x => x).Count();
            int totalQuestions = data.isTrue.Count();

            return Json(new { redirectToUrl = Url.Action("GetLastScore", "Score", new { correct = correctAnswers, questCount = totalQuestions }) });
        }
    }
}
