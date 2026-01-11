// Program.cs
using TaskManagment_Rest_API.DTOs;
using TaskManagment_Rest_API.Models;
using TaskManagment_Rest_API.Repositories.Implementations;
using TaskManagment_Rest_API.Repositories.Interfaces;
using TaskManagment_Rest_API.Services.Implementations;
using TaskManagment_Rest_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Dependency Injection - регистраци€ зависимостей
// Singleton - один экземпл€р на всЄ приложение (дл€ простоты, в реальном проекте Scoped)
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<IMapper<TaskItem, TaskResponseDto>, TaskMapper>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Swagger дл€ документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "REST API with SOLID principles demonstration"
    });
});

// CORS (если нужно дл€ фронтенда)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
        c.RoutePrefix = string.Empty; // Swagger на главной странице
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();