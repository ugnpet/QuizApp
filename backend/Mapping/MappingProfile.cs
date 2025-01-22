using AutoMapper;
using Models;

namespace Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quiz, QuizDTO>();
        CreateMap<Result, ResultDTO>();
    }
}

public class QuizDTO
{
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = "text";
    public List<string> Options { get; set; } = new List<string>();
}

public class ResultDTO
{
    public string UserEmail { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TimeInSeconds { get; set; }
}
