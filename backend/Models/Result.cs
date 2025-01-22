namespace Models;
public class Result
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TimeInSeconds { get; set; }
    public List<SubmittedAnswer> Answers { get; set; } = new List<SubmittedAnswer>();
}

public class SubmittedAnswer
{
    public int QuizId { get; set; }
    public string Answer { get; set; } = string.Empty;
}
