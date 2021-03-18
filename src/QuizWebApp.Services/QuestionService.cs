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
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext db;

        public QuestionService(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
