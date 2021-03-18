using Microsoft.EntityFrameworkCore;
using QuizWebApp.Data;
using QuizWebApp.Models;
using QuizWebApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuizWebApp.Tests
{
    public class CategoryServiceTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly CategoryService categoryService;

        public CategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.categoryService = new CategoryService(this.dbContext);
        }

        [Fact]
        public async Task GetAllCategoriesFromDb_WithSeededData_ShouldReturnAllCategoriesCorrectly()
        {
            await this.SeedCategoriesInDbAsync();

            var expected = this.categoryService.GetAllCategoriesFromDb();

            Assert.Equal(13, expected.Count);
        }

        [Fact]
        public async Task GetFiveDifferentCategoryIds_WithSeededData_ShouldReturnCorrectly()
        {
            await this.SeedCategoriesInDbAsync();

            var result = await this.categoryService.GetFiveDifferentCategoryIds();

            Assert.Equal(5, result.Count);
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
