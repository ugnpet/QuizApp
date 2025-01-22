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
            Text = "Select primary colors",
            Type = QuizType.Checkbox,
            Options = new List<string> { "Red", "Blue", "Yellow", "Green" },
            CorrectOptions = new List<string> { "Red", "Blue", "Yellow" }
        },
        new Quiz
        {
            Id = 3,
            Text = "What is the capital of France?",
            Type = QuizType.Text,
            Options = new List<string> { "Paris" },
            CorrectOptions = new List<string> { "Paris" }
        },
        new Quiz
        {
            Id = 4,
            Text = "Which of these are fruits?",
            Type = QuizType.Checkbox,
            Options = new List<string> { "Apple", "Carrot", "Banana", "Potato" },
            CorrectOptions = new List<string> { "Apple", "Banana" }
        }
    );

    context.SaveChanges();
}

app.Run();
