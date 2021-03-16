using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        public ICollection<QuizCategory> Quizzes { get; set; } = new HashSet<QuizCategory>();

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();

        public ICollection<Score> Scores { get; set; } = new HashSet<Score>();
    }
}
