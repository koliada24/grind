using ExampleMinimal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDB>(options => options.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

app.MapGet("/todoitems", async (TodoDB db) =>
    await db.TodoItems.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDB db) =>
await db.TodoItems.FindAsync(id)
    is TodoItem todoItem
        ? Results.Ok(todoItem)
        : Results.NotFound());

app.MapPost("/todoitems", async (TodoItem todoItem, TodoDB db) =>
{
    db.TodoItems.Add(todoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", todoItem);
});

app.MapPut("/todoitems/{id}", async (int id, TodoItem inputTodoItem, TodoDB db) =>
{
    var todoItem = await db.TodoItems.FindAsync(id);
    if (todoItem is null) return Results.NotFound();
    todoItem.Name = inputTodoItem.Name;
    todoItem.IsCompleted = inputTodoItem.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();