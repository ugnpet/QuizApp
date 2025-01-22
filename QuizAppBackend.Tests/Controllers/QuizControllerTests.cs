using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Controllers;
using Models;
using QuizAppBackend.Tests.Helpers;
using AutoMapper;
using Mapping;

public class QuizControllerTests
{
    [Fact]
    public void GetQuizzes_ReturnsAllQuizzes()
    {
        var context = DbContextMock.GetInMemoryDbContext();
        var mockMapper = new Mock<IMapper>();

        mockMapper.Setup(m => m.Map<List<QuizDTO>>(It.IsAny<List<Quiz>>()))
                  .Returns(new List<QuizDTO>
                  {
                      new QuizDTO { Text = "What is 2 + 2?", Type = "radio", Options = new List<string> { "3", "4", "5" } },
                      new QuizDTO { Text = "Select primary colors", Type = "checkbox", Options = new List<string> { "Red", "Blue", "Yellow", "Green" } }
                  });

        var controller = new QuizController(context, mockMapper.Object);
        var result = controller.GetQuizzes();
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var quizzes = Assert.IsType<List<QuizDTO>>(actionResult.Value);
        Assert.Equal(2, quizzes.Count);
    }

    [Fact]
    public void GetQuizzes_ReturnsEmptyList()
    {
        var context = DbContextMock.GetInMemoryDbContext();
        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<List<QuizDTO>>(It.IsAny<List<Quiz>>())).Returns(new List<QuizDTO>());

        var controller = new QuizController(context, mockMapper.Object);
        var result = controller.GetQuizzes();
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var quizzes = Assert.IsType<List<QuizDTO>>(actionResult.Value);
        Assert.Empty(quizzes);
    }
}
