using CashingWithPaginationDemo.Src;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=database.db");
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}

app.MapGet("/data", async (AppDbContext dbContext) =>
{
    var timer = new Stopwatch();
    timer.Start();

    var data = await dbContext.EntityModels.ToListAsync();

    timer.Stop();
    var formattedTime = timer.Elapsed.ToSecondsString();
    var result = new RequestResult(formattedTime, data);
    return result;
});

app.Run();