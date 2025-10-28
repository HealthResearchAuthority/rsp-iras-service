using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Interceptors;

namespace Rsp.IrasService.Configuration.Database;

/// <summary>
/// Adds DbContext to the application
/// </summary>
public static class DatabaseConfiguration
{
    /// <summary>
    /// Adds services to the IoC container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IrasContext>
        (
            (serviceProvider, options) => options
                .UseSqlServer(configuration.GetConnectionString("IrasServiceDatabaseConnection"))
                .AddInterceptors(serviceProvider.GetRequiredService<RegulatoryBodyAuditTrailInterceptor>())
                .AddInterceptors(serviceProvider.GetRequiredService<SponsorOrganisationAuditTrailInterceptor>())
        );

        return services;
    }
}