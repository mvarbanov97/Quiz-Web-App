using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public ICollection<Quiz> Quzzes { get; set; } = new HashSet<Quiz>();

        public ICollection<Score> Scores { get; set; } = new HashSet<Score>();
    }
}
