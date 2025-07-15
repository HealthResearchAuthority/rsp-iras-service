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

        // services
        services.AddTransient<IApplicationsService, ApplicationsService>();
        services.AddTransient<IRespondentService, RespondentService>();
        services.AddTransient<ITriggerEmailNotificationService, TriggerEmailNotificationService>();
        services.AddTransient<IMessageQueueService, AzureMessageQueueService>();
        services.AddTransient<IEventTypeService, EventTypeService>();
        services.AddTransient<IEmailTemplateService, EmailTemplateService>();
        services.AddTransient<IReviewBodyService, ReviewBodyService>();
        services.AddTransient<IReviewBodyAuditTrailService, ReviewBodyAuditTrailService>();
        services.AddTransient<IAuditTrailDetailsService, AuditTrailDetailsService>();
        services.AddTransient<IProjectModificationService, ProjectModificationService>();

        // repositories
        services.AddTransient<IProjectRecordRepository, ProjectRecordRepository>();
        services.AddTransient<IProjectPersonnelRepository, RespondentRepository>();
        services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddTransient<IEventTypeRepository, EventTypeRepository>();
        services.AddTransient<IRegulatoryBodyRepository, RegulatoryBodyRepository>();
        services.AddTransient<IRegulatoryBodyAuditTrailRepository, RegulatoryBodyAuditTrailRepository>();
        services.AddTransient<IProjectModificationRepository, ProjectModificationRepository>();

        // handlers and interceptors
        services.AddTransient<IAuditTrailHandler, ReviewBodyAuditTrailHandler>();
        services.AddTransient<AuditTrailInterceptor>();

        services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<IApplication>());

        return services;
    }
}