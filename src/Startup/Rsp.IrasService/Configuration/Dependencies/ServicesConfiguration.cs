namespace Rsp.IrasService.Configuration.Dependencies;

/// <summary>
///  User Defined Services Configuration
/// </summary>
[ExcludeFromCodeCoverage]
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

        services.AddTransient<IRespondentService, RespondentService>();
        services.AddTransient<IRespondentRepository, RespondentRepository>();

        services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<IApplication>());

        return services;
    }
}