using BlazorAppDemo.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAppDemo.Persistence;

public static class DependenciesBootstrapper
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlazorDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(BlazorDbContext).Assembly.FullName));

            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        services.AddScoped<IBlazorDbContext>(provider => provider.GetService<BlazorDbContext>());

        return services;
    }
}