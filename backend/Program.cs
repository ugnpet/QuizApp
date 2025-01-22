using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models;
using Mapping;
using Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseInMemoryDatabase("QuizDatabase"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers(); // Map controller routes

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

    context.Quizzes.AddRange(
        new Quiz
        {
            Id = 1,
            Text = "What is 2 + 2?",
            Type = QuizType.Radio,
            Options = new List<string> { "3", "4", "5" },
            CorrectOptions = new List<string> { "4" }
        },
        new Quiz
        {
            Id = 2,
            Text = "Select the primary colors",
            Type = QuizType.Checkbox,
            Options = new List<string> { "Red", "Blue", "Yellow", "Green" },
            CorrectOptions = new List<string> { "Red", "Blue", "Yellow" }
        },
        new Quiz
        {
            Id = 3,
            Text = "Which duck is known for its distinctive whistling call?",
            Type = QuizType.Radio,
            Options = new List<string> { "Muscovy duck", "Mallard", "Pekin duck", "Teal" },
            CorrectOptions = new List<string> { "Muscovy duck" }
        },
        new Quiz
        {
            Id = 4,
            Text = "Which planet is known as the Red Planet?",
            Type = QuizType.Radio,
            Options = new List<string> { "Earth", "Mars", "Jupiter" },
            CorrectOptions = new List<string> { "Mars" }
        },
        new Quiz
        {
            Id = 5,
            Text = "What is the capital of Italy?",
            Type = QuizType.Text,
            Options = new List<string> { "Rome" },
            CorrectOptions = new List<string> { "Rome" }
        },
        new Quiz
        {
            Id = 6,
            Text = "What type of duck is famous for its colorful plumage?",
            Type = QuizType.Radio,
            Options = new List<string> { "Mandarin duck", "Wood duck", "Teal duck", "Muscovy duck" },
            CorrectOptions = new List<string> { "Mandarin duck" }
        },
        new Quiz
        {
            Id = 7,
            Text = "Name the author of '1984'.",
            Type = QuizType.Text,
            Options = new List<string> { "George Orwell" },
            CorrectOptions = new List<string> { "George Orwell" }
        },
        new Quiz
        {
            Id = 8,
            Text = "Which of the following are fruits?",
            Type = QuizType.Checkbox,
            Options = new List<string> { "Apple", "Carrot", "Banana", "Potato" },
            CorrectOptions = new List<string> { "Apple", "Banana" }
        },
        new Quiz
        {
            Id = 9,
            Text = "Which of the following are programming languages?",
            Type = QuizType.Checkbox,
            Options = new List<string> { "Python", "HTML", "Java", "CSS" },
            CorrectOptions = new List<string> { "Python", "Java" }
        },
        new Quiz
        {
            Id = 10,
            Text = "Which of the following statements about ducks is true?",
            Type = QuizType.Checkbox,
            Options = new List<string>
            {
            "Ducks can fly long distances",
            "All ducks are freshwater birds",
            "Ducks are exclusively herbivores",
            "Male ducks are called drakes"
            },
            CorrectOptions = new List<string> { "Ducks can fly long distances", "Male ducks are called drakes" }
        }
    );

    context.SaveChanges();

}

app.Run();
