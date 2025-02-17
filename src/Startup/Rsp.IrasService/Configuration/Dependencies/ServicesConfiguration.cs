using Azure.Messaging.ServiceBus;
using Rsp.IrasService.Application;
using Rsp.IrasService.Application.Authentication.Helpers;
using Rsp.IrasService.Application.Contracts;
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

        services.AddTransient<IRespondentService, RespondentService>();
        services.AddTransient<IRespondentRepository, RespondentRepository>();
        services.AddTransient<IEmailNotificationService, EmailNotificationService>();
        services.AddTransient<IMessageQueueService, AzureMessageQueueService>();
        services.AddTransient<IEventTypeService, EventTypeService>();
        services.AddTransient<IEmailTemplateService, EmailTemplateService>();
        services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddTransient<IEventTypeRepository, EventTypeRepository>();

        services.AddSingleton<ServiceBusClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            string connectionString = configuration["AppSettings:AzureServiceBus:ConnectionString"];
            return new ServiceBusClient(connectionString);
        });

        services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<IApplication>());

        return services;
    }
}