using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Models;
using Mapping;
using Data;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly QuizDbContext _context;
    private readonly IMapper _mapper;

    public ResultsController(QuizDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // POST /api/results/submit
    [HttpPost("submit")]
    public ActionResult SubmitResult([FromBody] Result result)
    {
        if (result == null || string.IsNullOrWhiteSpace(result.UserEmail) || result.Answers == null || !result.Answers.Any())
        {
            return BadRequest(new { Message = "Invalid submission data" });
        }

        int score = 0;

        var quizzes = _context.Quizzes.ToDictionary(q => q.Id);

        foreach (var answer in result.Answers ?? Enumerable.Empty<SubmittedAnswer>())
        {
            if (!quizzes.TryGetValue(answer.QuizId, out var quiz)) continue;

            switch (quiz.Type)
            {
                case QuizType.Radio:
                    if (quiz.CorrectOptions.Contains(answer.Answer)) score += 100;
                    break;

                case QuizType.Checkbox:
                    var selectedAnswers = answer.Answer.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var correctChecked = selectedAnswers.Count(opt => quiz.CorrectOptions.Contains(opt));
                    var incorrectChecked = selectedAnswers.Count(opt => !quiz.CorrectOptions.Contains(opt));

                    if (correctChecked > 0 && incorrectChecked == 0)
                    {
                        score += (int)Math.Ceiling((100.0 / quiz.CorrectOptions.Count) * correctChecked);
                    }
                    break;

                case QuizType.Text:
                    if (quiz.CorrectOptions.Any(opt => string.Equals(opt, answer.Answer, StringComparison.OrdinalIgnoreCase)))
                    {
                        score += 100;
                    }
                    break;

                default:
                    break;
            }
        }

        result.Score = score;
        result.SubmittedAt = DateTime.UtcNow;
        _context.Results.Add(result);
        _context.SaveChanges();

        return Ok(new { Message = "Result submitted successfully", Score = result.Score });
    }

    // GET /api/results/highscores
    [HttpGet("highscores")]
    public ActionResult<List<ResultDTO>> GetHighScores([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0) return BadRequest(new { Message = "Page and pageSize must be greater than zero" });

        var topResults = _context.Results
            .OrderByDescending(r => r.Score)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var resultDTOs = _mapper.Map<List<ResultDTO>>(topResults);
        return Ok(resultDTOs);
    }

}
