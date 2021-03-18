using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models.API_Models
{
    public class TriviaCategoeries
    {
        [JsonProperty("trivia_categories")]
        public List<Category> Categories { get; set; }
    }
}
