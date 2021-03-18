using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [JsonProperty("question")]
        public string Content { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [JsonProperty("category")]
        public string CategoryName { get; set; }

        [NotMapped]
        [JsonProperty("incorrect_answers")]
        public ICollection<string> Options { get; set; } = new HashSet<string>();

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        public ICollection<QuizQuestion> Quizzes { get; set; } = new HashSet<QuizQuestion>();
    }
}
