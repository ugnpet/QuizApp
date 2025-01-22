using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Models;
using Mapping;
using Data;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly QuizDbContext _context;
    private readonly IMapper _mapper;

    public QuizController(QuizDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET /api/quiz
    [HttpGet]
    public ActionResult<List<QuizDTO>> GetQuizzes()
    {
        var quizzes = _context.Quizzes.ToList();
        var quizDTOs = _mapper.Map<List<QuizDTO>>(quizzes);
        return Ok(quizDTOs);
    }
}
