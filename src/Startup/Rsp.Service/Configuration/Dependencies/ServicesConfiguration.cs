using Rsp.Service.Application;
using Rsp.Service.Application.Authentication.Helpers;
using Rsp.Service.Application.Behaviors;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure.Helpers;
using Rsp.Service.Infrastructure.Interceptors;
using Rsp.Service.Infrastructure.Middlewares;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.Configuration.Dependencies;

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
        services.AddTransient<IDocumentService, DocumentService>();
        services.AddTransient<ISponsorOrganisationsService, SponsorOrganisationsService>();
        services.AddTransient<IProjectClosureService, ProjectClosureService>();

        // repositories
        services.AddTransient<IProjectRecordRepository, ProjectRecordRepository>();
        services.AddTransient<IProjectPersonnelRepository, RespondentRepository>();
        services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddTransient<IEventTypeRepository, EventTypeRepository>();
        services.AddTransient<IRegulatoryBodyRepository, RegulatoryBodyRepository>();
        services.AddTransient<IRegulatoryBodyAuditTrailRepository, RegulatoryBodyAuditTrailRepository>();
        services.AddTransient<IProjectModificationRepository, ProjectModificationRepository>();
        services.AddTransient<IDocumentRepository, DocumentRepository>();
        services.AddTransient<ISponsorOrganisationsRepository, SponsorOrganisationRepository>();
        services.AddTransient<IProjectClosureRepository, ProjectClosureRepository>();

        // Access validation repository and middleware
        services.AddTransient<IAccessValidationRepository, AccessValidationRepository>();
        services.AddTransient<ProjectRecordAccessValidationMiddleware>();
        services.AddTransient<ModificationAccessValidationMiddleware>();
        services.AddTransient<DocumentAccessValidationMiddleware>();

        // handlers and interceptors
        // Handlers (generic)
        services.AddScoped<IAuditTrailHandler<SponsorOrganisationAuditTrail>, SponsorOrganisationAuditTrailHandler>();
        services.AddScoped<IAuditTrailHandler<SponsorOrganisationAuditTrail>, SponsorOrganisationUserAuditTrailHandler>();

        services.AddScoped<IAuditTrailHandler<RegulatoryBodyAuditTrail>, ReviewBodyAuditTrailHandler>();
        services.AddScoped<IAuditTrailHandler<RegulatoryBodyAuditTrail>, RegulatoryBodyUserAuditTrailHandler>();

        services.AddScoped<IAuditTrailHandler<ProjectModificationAuditTrail>, ProjectModificationAuditTrailHandler>();

        services.AddScoped<IAuditTrailHandler<ProjectRecordAuditTrail>, ProjectRecordAuditTrailHandler>();

        // Single merged interceptor
        services.AddScoped<AuditTrailInterceptor>();

        // Register IHttpContextAccessor used by pipeline to read user claims
        services.AddHttpContextAccessor();

        // Register MediatR and pipeline behaviours
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssemblyContaining<IApplication>();

            // Pipeline behaviours for AllowedStatuses handling
            option.AddOpenBehavior(typeof(AllowedStatusesPopulationBehavior<,>));
        });

        return services;
    }
}