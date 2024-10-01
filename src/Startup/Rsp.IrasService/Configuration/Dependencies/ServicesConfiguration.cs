using Rsp.IrasService.Application;
using Rsp.IrasService.Application.Authentication.Helpers;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
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
        services.AddSingleton<ITokenHelper, TokenHelper>();

        services.AddTransient<IApplicationsService, ApplicationsService>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<IApplication>());

        return services;
    }
}