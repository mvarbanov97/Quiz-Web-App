using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models.API_Models
{
    public class QuestionInfo
    {
        [Required]
        [JsonProperty("question")]
        public string Content { get; set; }

        [JsonProperty("category")]
        public string CategoryName { get; set; }

        [JsonProperty("incorrect_answers")]
        public IList<string> Options { get; set; } = new List<string>();

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }
    }
}
