using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models
{
    public class Score
    {
        public int CorrectAnswers { get; set; }

        public int WrongAnswers { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
