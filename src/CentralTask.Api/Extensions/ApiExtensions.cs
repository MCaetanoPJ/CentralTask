using CentralTask.Application;
using CentralTask.Core;
using CentralTask.Core.AppSettingsConfigurations;
using CentralTask.Core.Middlewares;
using CentralTask.Core.Settings;
using CentralTask.DI;
using CentralTask.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Serilog;

namespace CentralTask.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            opt.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

        var jwtSettingsSection = configuration.GetSection(JwtSettings.Section);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

        if (jwtSettings is null) throw new Exception("Configuração SMPT necessária");

        services
            .AddSwaggerConfiguration()
            .AddIdentityConfiguration()
            .AddJwtAuthentication(jwtSettings)
            .AddServices(configuration)
            .AddCqrs(typeof(IAssemblyMarker).Assembly)
            .AddPolicies();

        services.Configure<JwtSettings>(jwtSettingsSection);
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddLogging(logging => logging.AddSerilog());

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app, IConfiguration configuration)
    {
        if (app.Environment.IsDevelopmentOrInternal())
        {
            app.UseDeveloperExceptionPage();

            var migrator = app.Services.GetRequiredService<Migrator>();

            migrator.Migrate().Wait();
        }

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        var configSwaggerSection = configuration.GetSection("ConfigSwagger");
        var configSwagger = configSwaggerSection.Get<ConfigSwagger>();
        app.UseSwaggerConfiguration(configSwagger);

        app.UseRequestLocalization("pt-BR");

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }

    public static bool IsDevelopmentOrInternal(this IWebHostEnvironment environment)
    {
        return environment.IsDevelopment() || environment.EnvironmentName == "Internal";
    }
}
