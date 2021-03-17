using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Services.ServiceModels
{
    public class ScoreServiceModel
    {
        public int CorrectAnswersCount { get; set; }

        public int WrongAnswersCount { get; set; }

        public double Average { get; set; }
    }
}
