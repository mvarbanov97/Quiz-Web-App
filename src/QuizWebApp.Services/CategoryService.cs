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
    }
}
