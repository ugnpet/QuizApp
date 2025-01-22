using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Controllers;
using Models;
using QuizAppBackend.Tests.Helpers;
using AutoMapper;
using Mapping;

public class ResultControllerTests
{
    [Fact]
    public void SubmitResult_CalculatesCorrectScore()
    {
        var context = DbContextMock.GetInMemoryDbContext();
        var controller = new ResultsController(context, null);

        var result = new Result
        {
            UserEmail = "test@example.com",
            Answers = new List<SubmittedAnswer>
        {
            new SubmittedAnswer { QuizId = 1, Answer = "4" },
            new SubmittedAnswer { QuizId = 2, Answer = "Red,Blue,Yellow" }
        }
        };
        var response = controller.SubmitResult(result);
        var actionResult = Assert.IsType<OkObjectResult>(response);

        var responseObject = actionResult.Value;
        Assert.NotNull(responseObject);
        Assert.True(responseObject.GetType().GetProperty("Message") != null);
        Assert.True(responseObject.GetType().GetProperty("Score") != null);
        Assert.Equal("Result submitted successfully", responseObject.GetType().GetProperty("Message")?.GetValue(responseObject));
        Assert.Equal(200, responseObject.GetType().GetProperty("Score")?.GetValue(responseObject));
    }

    [Fact]
    public void SubmitResult_IgnoresInvalidQuizId()
    {
        var context = DbContextMock.GetInMemoryDbContext();
        var controller = new ResultsController(context, null);

        var result = new Result
        {
            UserEmail = "test@example.com",
            Answers = new List<SubmittedAnswer>
        {
            new SubmittedAnswer { QuizId = 999, Answer = "WrongId" }
        }
        };

        var response = controller.SubmitResult(result);
        var actionResult = Assert.IsType<OkObjectResult>(response);
        var responseObject = actionResult.Value;
        Assert.NotNull(responseObject);
        Assert.True(responseObject.GetType().GetProperty("Message") != null);
        Assert.True(responseObject.GetType().GetProperty("Score") != null);
        Assert.Equal("Result submitted successfully", responseObject.GetType().GetProperty("Message")?.GetValue(responseObject));
        Assert.Equal(0, responseObject.GetType().GetProperty("Score")?.GetValue(responseObject));
    }

    [Fact]
    public void GetHighScores_ReturnsTop10Results()
    {
        var context = DbContextMock.GetInMemoryDbContext();

        context.Results.AddRange(
            Enumerable.Range(1, 15).Select(i => new Result
            {
                Id = i,
                UserEmail = $"user{i}@example.com",
                Score = i * 10
            })
        );
        context.SaveChanges();

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<List<ResultDTO>>(It.IsAny<List<Result>>()))
                  .Returns(context.Results.OrderByDescending(r => r.Score).Take(10).Select(r => new ResultDTO
                  {
                      UserEmail = r.UserEmail,
                      Score = r.Score,
                      TimeInSeconds = r.TimeInSeconds
                  }).ToList());

        var controller = new ResultsController(context, mockMapper.Object);

        var result = controller.GetHighScores();
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var highScores = Assert.IsType<List<ResultDTO>>(actionResult.Value);
        Assert.Equal(10, highScores.Count);
        Assert.Equal(150, highScores.First().Score);
    }

    [Fact]
    public void SubmitResult_HandlesEmptyAnswers()
    {
        var context = DbContextMock.GetInMemoryDbContext();
        var controller = new ResultsController(context, null);

        var result = new Result
        {
            UserEmail = "test@example.com",
            Answers = new List<SubmittedAnswer>() 
        };

        var response = controller.SubmitResult(result);
        var actionResult = Assert.IsType<BadRequestObjectResult>(response); 
        var responseObject = actionResult.Value;

        Assert.NotNull(responseObject);
        Assert.True(responseObject.GetType().GetProperty("Message") != null); 
        Assert.Equal("Invalid submission data", responseObject.GetType().GetProperty("Message")?.GetValue(responseObject));
    }

}