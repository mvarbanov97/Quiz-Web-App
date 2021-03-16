using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models
{
    public class QuizQuestion
    {
        public Quiz Quiz { get; set; }
        public int QuizId { get; set; }

        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
