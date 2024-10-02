using Microsoft.EntityFrameworkCore;
using Test.App;
using Test.Persistence;

namespace Test.API.ServiceCollection;

public static class EFInfrastructure
{
    public static IServiceCollection AddProjectDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = typeof(TestDbContext).Assembly;

        var connectionString = configuration.GetConnectionString("Npgsql");

        services.AddDbContext<TestDbContext>(options =>
        {
            options.UseNpgsql(connectionString, o =>
            {
                o.MigrationsAssembly(migrationsAssembly.GetName().Name);
                o.EnableRetryOnFailure(15);
            }).UseSnakeCaseNamingConvention();

            options.EnableSensitiveDataLogging();
        }, ServiceLifetime.Scoped);

        services.AddScoped<ITestDbContext>(sp => sp.GetRequiredService<TestDbContext>());

        return services;
    }
    public static void DatabaseMigrate(this WebApplication app)
    {
        var serviceProvider = app.Services.GetRequiredService<IServiceProvider>();

        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();

        context.Database.Migrate();
    }
}