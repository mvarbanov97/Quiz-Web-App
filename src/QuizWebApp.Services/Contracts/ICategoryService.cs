using QuizWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services.Contracts
{
    public interface ICategoryService : IService
    {
        ICollection<Category> GetAllCategoriesFromDb();

        Task<List<int>> GetFiveDifferentCategoryIds();
    }
}
