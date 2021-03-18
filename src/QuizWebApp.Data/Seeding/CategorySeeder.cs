using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizWebApp.Data.Seeding.Contracts;
using QuizWebApp.Models;
using QuizWebApp.Models.API_Models;
using QuizWebApp.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Seeding
{
    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await this.LoadGenresInDbAsync(dbContext);
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


        /// <summary>
        /// An async method which loads Categories from Trivia API into the database.
        /// <para>API Documentation <see href="https://opentdb.com/api_config.php">HERE</see></para>
        /// </summary>
        /// <returns>
        /// A Task representing an asyncronous operation which fills the database with Categories.
        /// </returns>
        public async Task LoadGenresInDbAsync(ApplicationDbContext db)
        {
            if (this.DatabaseContainsCategories(db))
                return;

            var apiObject = await this.GetCategoriesFromAPIAsync();

            using (var transaction = db.Database.BeginTransaction())
            {
                var categories = apiObject.Categories;
                await db.AddRangeAsync(categories);

                // Executing the command below as the Trivia API is setting the Id of the category 
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT QuizWebApp.dbo.Categories ON;");

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        /// <summary>
        /// An method which checks if there are any Categories in the Database
        /// <para>Used when the user selects a category for which he wants to see his results</para>
        /// </summary>
        /// <returns>
        /// A boolean which is true if there are Categories in the Database and false if there are not.
        /// </returns>
        private bool DatabaseContainsCategories(ApplicationDbContext db)
        {
            if (db.Categories == null || db.Categories.Count() == 0)
                return false;

            return true;
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
    }
}
