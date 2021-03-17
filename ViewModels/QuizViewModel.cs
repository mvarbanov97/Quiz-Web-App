using Newtonsoft.Json;
using QuizWebApp.Models;
using QuizWebApp.Models.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.ViewModels
{
    public class QuizViewModel
    {
        public List<QuestionInfo> Questions { get; set; } = new List<QuestionInfo>();
    }
}
