using Rsp.IrasService.Application;
using Rsp.IrasService.Application.Authentication.Helpers;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Infrastructure.Helpers;
using Rsp.IrasService.Infrastructure.Interceptors;
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

        services.AddTransient<IRespondentService, RespondentService>();
        services.AddTransient<IRespondentRepository, RespondentRepository>();
        services.AddTransient<ITriggerEmailNotificationService, TriggerEmailNotificationService>();
        services.AddTransient<IMessageQueueService, AzureMessageQueueService>();
        services.AddTransient<IEventTypeService, EventTypeService>();
        services.AddTransient<IEmailTemplateService, EmailTemplateService>();
        services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddTransient<IEventTypeRepository, EventTypeRepository>();
        services.AddTransient<IReviewBodyService, ReviewBodyService>();
        services.AddTransient<IRegulatoryBodyRepository, ReviewBodyRepository>();
        services.AddTransient<IRegulatoryBodyAuditTrailRepository, ReviewBodyAuditTrailRepository>();
        services.AddTransient<IReviewBodyAuditTrailService, ReviewBodyAuditTrailService>();
        services.AddTransient<IAuditTrailHandler, ReviewBodyAuditTrailHandler>();
        services.AddTransient<IAuditTrailDetailsService, AuditTrailDetailsService>();
        services.AddTransient<AuditTrailInterceptor>();

        services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<IApplication>());

        return services;
    }
}