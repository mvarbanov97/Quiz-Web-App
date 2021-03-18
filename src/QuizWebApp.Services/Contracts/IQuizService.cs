using QuizWebApp.Common.Dtos;
using QuizWebApp.Models;
using QuizWebApp.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services.Contracts
{
    public interface IQuizService : IService
    {
        Task<QuizViewModel> GenerateRandomQuizWithDifferentCategories();

        void Shuffle<T>(IList<T> list);

        Task SubmitQuizToDb(User currentUser, ScoreDto data);
    }
}
