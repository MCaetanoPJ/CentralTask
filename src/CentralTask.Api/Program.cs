using CentralTask.Api.Extensions;
using CentralTask.Api.Middleware;
using Newtonsoft.Json;
using Serilog;
using CentralTask.Broker;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Controllers com Newtonsoft
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Middlewares
builder.Services.AddTransient<ContentValidationMiddleware>();

// Configuração padrão da API
builder.Services.AddApiServicesConfiguration(builder.Configuration);

// Adiciona SignalR
builder.Services.AddSignalR();

// RabbitMQ (ajustado para CreateConnectionAsync)
builder.Services.AddSingleton<EventConnection>(_ =>
    new EventConnection(builder.Configuration.GetValue<string>("RabbitMQ:Host") ?? "localhost"));

builder.Services.AddSingleton<EventPublisher>();
builder.Services.AddSingleton<EventConsumer>();

var app = builder.Build();

app.UseWebSockets();
app.UseMiddleware<ContentValidationMiddleware>();

// Configuração de rotas e middlewares padrão
app.UseApiConfiguration(builder.Configuration);

// Mapeia o hub do SignalR
//app.MapHub<NotificationHub>("/hubs/notifications");

// Inicializa o consumidor do RabbitMQ assincronamente
var consumer = app.Services.GetRequiredService<EventConsumer>();
_ = consumer.ConsumerAsync("MinhaFila"); // "MinhaFila" pode vir de appsettings.json

app.Run();
