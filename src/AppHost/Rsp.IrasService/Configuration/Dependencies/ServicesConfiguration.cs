using Rsp.IrasService.Application.AuthenticationHelpers;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Repositories;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.Configuration.Dependencies;

/// <summary>
///  User Defined Services Configuration
/// </summary>
public static class ServicesConfiguration
{
    /// <summary>
    /// Adds services to the IoC container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoriesService, CategoriesService>();
        services.AddSingleton<ITokenHelper, TokenHelper>();

        services.AddTransient<IApplicationsService, ApplicationsService>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();

        return services;
    }
}