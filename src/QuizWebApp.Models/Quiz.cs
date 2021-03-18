using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public ICollection<QuizCategory> Categories { get; set; } = new HashSet<QuizCategory>();

        public ICollection<QuizQuestion> Questions { get; set; } = new HashSet<QuizQuestion>();
    }
}
