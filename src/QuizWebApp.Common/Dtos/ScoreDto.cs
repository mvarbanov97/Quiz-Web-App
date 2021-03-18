using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Common.Dtos
{
    public class ScoreDto
    {
        public ICollection<string> categories { get; set; } = new HashSet<string>();

        public ICollection<bool> isTrue { get; set; } = new HashSet<bool>();
    }
}
