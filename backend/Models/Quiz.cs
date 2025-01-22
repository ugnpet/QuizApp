namespace Models
{
    public enum QuizType
    {
        Radio,
        Checkbox,
        Text
    }

    public class Quiz
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public QuizType Type { get; set; }
        public required List<string> Options { get; set; }
        public required List<string> CorrectOptions { get; set; }
    }
}
