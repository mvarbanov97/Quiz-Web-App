using Microsoft.EntityFrameworkCore;
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
    public class CategoryService : ICategoryService
    {
        //TODO: Add Automapper so I can map Domain Models with Dtos, Service Models, Api Objects
        //TODO: Improve Api Objects, DTO's, Service Models
        private readonly ApplicationDbContext db;

        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// An async method which loads Categories from Trivia API into the database.
        /// <para>API Documentation <see href="https://opentdb.com/api_config.php">HERE</see></para>
        /// </summary>
        /// <returns>
        /// A Task representing an asyncronous operation which fills the database with Categories.
        /// </returns>
        public async Task LoadGenresInDbAsync()
        {
            if (this.DatabaseContainsCategories())
                return;

            var apiObject = await this.GetCategoriesFromAPIAsync();

            using (var transaction = this.db.Database.BeginTransaction())
            {
                var categories = apiObject.Categories;
                await this.db.AddRangeAsync(categories);

                // Executing the command below as the Trivia API is setting the Id of the category 
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT QuizWebApp.dbo.Categories ON;");

                await this.db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        /// <summary>
        /// An method which fetch all of the categories from the Database.
        /// <para>Used when the user selects a category for which he wants to see his results</para>
        /// </summary>
        /// <returns>
        /// A ICollection with all of the Categories from the Database.
        /// </returns>
        public ICollection<Category> GetAllCategoriesFromDb()
        {
            var allCategories = this.db.Categories.ToList();

            return allCategories;
        }

        /// <summary>
        /// An method which checks if there are any Categories in the Database
        /// <para>Used when the user selects a category for which he wants to see his results</para>
        /// </summary>
        /// <returns>
        /// A boolean which is true if there are Categories in the Database and false if there are not.
        /// </returns>
        private bool DatabaseContainsCategories()
        {
            if (this.db.Categories == null || this.db.Categories.Count() == 0)
                return false;

            return true;
        }

        /// <summary>
        /// An async method which returns Categories from Trivia API.
        /// <para>API Documentation <see href="https://opentdb.com/api_config.php">HERE</see></para>
        /// </summary>
        /// <returns>
        /// Task<TriviaCategoeries> list of all categories in the API, Category Ids form 9 to 21
        /// </returns>
        private async Task<TriviaCategoeries> GetCategoriesFromAPIAsync()
        {
            string url = $"https://opentdb.com/api_category.php";

            JObject json = await this.GetJsonStreamFromUrlAsync(url);
            var apiObject = JsonConvert.DeserializeObject<TriviaCategoeries>(json.ToString());

            return apiObject;
        }
    }
}
