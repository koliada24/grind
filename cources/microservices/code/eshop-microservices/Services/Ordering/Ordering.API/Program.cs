using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicationServices();
builder.Services.AddInfrastractureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.Run();
