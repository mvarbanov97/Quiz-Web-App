using Microsoft.AspNetCore.Mvc.Rendering;
using QuizWebApp.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QuizWebApp.Sevices.Extensions
{
    public static class SelectListGenerator
    {
        /// <summary>
        /// Creates new IEnumerable of SelectListItem. Used in the Index action of the Score controller. Can find another way of doing it. Could improve
        /// </summary>
        /// <param name="categoryService">An interfaces of ICategoryService used to fetch all the categories from the Database</param>
        /// <returns>IEnumerable<SelectListItem></SelectListItem></returns>
        public static IEnumerable<SelectListItem> GetAllCategories(ICategoryService categoryService)
        {
            var categories = categoryService.GetAllCategoriesFromDb();

            var groups = new List<SelectListGroup>();
            foreach (var category in categories)
            {
                if (groups.All(g => g.Name != category.Name))
                {
                    groups.Add(new SelectListGroup { Name = category.Name });
                }
            }

            return categories.Select(x => new SelectListItem
            {
                
                Value = x.Id.ToString(),
                Text = x.Name,
            });
        }
    }
}
