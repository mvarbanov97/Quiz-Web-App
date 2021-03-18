using Microsoft.EntityFrameworkCore;
using Moq;
using QuizWebApp.Common.ViewModels;
using QuizWebApp.Data;
using QuizWebApp.Models;
using QuizWebApp.Models.APIModels;
using QuizWebApp.Services;
using QuizWebApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QuizWebApp.Tests
{
    public class QuizServiceTests
    {
        private QuizService quizService;
        private ApplicationDbContext dbContext;

        public QuizServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            var mockCategoryService = new Mock<CategoryService>(this.dbContext);

            this.quizService = new QuizService(dbContext, mockCategoryService.Object);
        }

        [Fact]
        public async Task GenerateRandomQuizWithDifferentCategories_WithCorrectInputData_ShouldCreateQuizViewModel()
        {
            await this.SeedCategoriesInDbAsync();

            var result = await this.quizService.GenerateRandomQuizWithDifferentCategories();

            var quiz = this.GenerateQuiz();

            Assert.Equal(quiz.Questions.Count, result.Questions.Count);
            Assert.IsType<QuizViewModel>(result);
        }


        private QuizViewModel GenerateQuiz()
        {
            var quiz = new QuizViewModel();


            for (int i = 1; i <= 5; i++)
            {
                var question = new QuestionInfo();
                quiz.Questions.Add(question);
            }

            return quiz;
        }

        private async Task SeedCategoriesInDbAsync()
        {
            var categories = new List<Category>();

            for (int i = 9; i <= 21; i++)
            {
                var category = new Category
                {
                    Id = i,
                    Name = $"SeededCategory{i}"
                };

                categories.Add(category);
            }
            await this.dbContext.Categories.AddRangeAsync(categories);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
