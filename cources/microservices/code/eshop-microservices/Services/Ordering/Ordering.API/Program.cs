var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicationServices();
builder.Services.AddInfrastractureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
