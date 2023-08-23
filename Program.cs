using Microsoft.AspNetCore.OpenApi;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace minimal_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            Todo[] todosItems = {
                new Todo(Id: 0, Title: "This is first title", IsCompleted : false),
                new Todo(Id : 1, Title: "This is second title", IsCompleted: true),
                new Todo(Id : 2, Title: "Another title", IsCompleted : false),
                new Todo(Id : 3, Title: "Just some title", IsCompleted: true),
            };

            app.MapGet("todos/", () =>
            {
                return todosItems;
            });
            app.MapPost("todos/", async (HttpRequest request) =>
            {
                StreamReader reader = new(request.Body);
                string? jsonString = await reader.ReadToEndAsync();

                Todo? todo = JsonSerializer.Deserialize<Todo>(jsonString);
                todosItems.Append(todo);

                return todo;
            });
            app.MapGet("todos/{todoId}", (int todoId) =>
            {
                return todosItems.First((Todo todo) => {
                    return todo.Id == todoId;
                });
            });


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
