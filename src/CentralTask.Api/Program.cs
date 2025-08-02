using CentralTask.Api.Extensions;
using CentralTask.Api.Middleware;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });


builder.Services.AddTransient<ContentValidationMiddleware>();

builder.Services.AddApiServicesConfiguration(builder.Configuration);

var app = builder.Build();

app.UseWebSockets();
app.UseMiddleware<ContentValidationMiddleware>();

app.UseApiConfiguration(builder.Configuration);

app.Run();
