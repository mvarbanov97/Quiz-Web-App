namespace QuizWebApp.Models
{
    public class QuizCategory
    {
        public Quiz Quiz { get; set; }
        public int QuizId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}