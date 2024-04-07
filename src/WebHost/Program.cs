using Application;
using Infrastructure.DependencyRegistration;
using Presentation.DependencyRegistration;
using Presentation.Middleware;

string corsPolicy = "CORSPOLICY";
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices(builder.Configuration, corsPolicy)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware(corsPolicy);

app.Run();
