using QuizWebApp.Models;
using QuizWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services.Contracts
{
    public interface IScoreService : IService
    {
        Task<ScoreViewModel> GetTotalScoreCount(User user);

        Task<ScoreViewModel> GetScoreByCategoryId(User user, int categoryId);
    }
}
