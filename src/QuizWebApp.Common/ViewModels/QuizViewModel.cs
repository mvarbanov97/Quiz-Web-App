using Newtonsoft.Json;
using QuizWebApp.Models;
using QuizWebApp.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Common.ViewModels
{
    public class QuizViewModel
    {
        public List<QuestionInfo> Questions { get; set; } = new List<QuestionInfo>();
    }
}
