using Microsoft.EntityFrameworkCore;
using Models;
using Data;

namespace QuizAppBackend.Tests.Helpers
{
    public static class DbContextMock
    {
        public static QuizDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid())
                .Options;

            var context = new QuizDbContext(options);

            context.Quizzes.AddRange(
                new Quiz { Id = 1, Text = "What is 2 + 2?", Type = QuizType.Radio, Options = new List<string> { "3", "4", "5" }, CorrectOptions = new List<string> { "4" } },
                new Quiz { Id = 2, Text = "Select primary colors", Type = QuizType.Checkbox, Options = new List<string> { "Red", "Blue", "Yellow", "Green" }, CorrectOptions = new List<string> { "Red", "Blue", "Yellow" } },
                new Quiz { Id = 3, Text = "What is the capital of France?", Type = QuizType.Text, Options = new List<string>(), CorrectOptions = new List<string> { "Paris" } }
            );

            context.SaveChanges();
            return context;
        }
    }
}
