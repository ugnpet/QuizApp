using System.ComponentModel.DataAnnotations;

namespace Models;
public class Result
{
    [Key]
    public string UserEmail { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TimeInSeconds { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;  
    public List<SubmittedAnswer> Answers { get; set; } = new List<SubmittedAnswer>();
}

public class SubmittedAnswer
{
    public int QuizId { get; set; }
    public string Answer { get; set; } = string.Empty;
}
