using CentralTask.Application.AutoMapper;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using CentralTask.Application.Services.Interfaces;
using CentralTask.Application.Services;
using CentralTask.Application.Identidade;
using CentralTask.Infra.Data.Repositories;
using CentralTask.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CentralTask.DI;

public static class DependencyInjector
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddSingleton<Migrator>()
            .AddScoped<IUsuarioLogado, UsuarioLogado>()
            .AddRepositories()
            .AddSharedServices()
            .AddAutoMapper(typeof(AutoMapperConfig))
            .AddDbContext(configuration)
        .AddHttpClient();

        return services;
    }

    public static IServiceCollection AddServicesHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddRepositories()
            .AddAutoMapper(typeof(AutoMapperConfig))
            .AddDbContext(configuration, serviceLifetime: ServiceLifetime.Transient);

        return services;
    }

    public static IServiceCollection AddServicesIntegrador(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddRepositories()
            .AddAutoMapper(typeof(AutoMapperConfig))
            .AddDbContext(configuration, ServiceLifetime.Transient);

        return services;
    }

    public static void SetLocalCulture(this IServiceCollection services)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        cultureInfo.NumberFormat.CurrencySymbol = "R$";

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }

    private static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IUSuarioService, UsuarioService>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();        
        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<CentralTaskContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }, serviceLifetime, serviceLifetime);

        return services;
    }
}
