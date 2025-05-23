using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.FeatureManagement;
using Microsoft.Net.Http.Headers;
using Rsp.IrasService.Application.Authentication.Helpers;
using Rsp.IrasService.Application.Authorization.Handlers;
using Rsp.IrasService.Application.Settings;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Configuration.Auth;

/// <summary>
/// Authentication and Authorization configuration
/// </summary>
public static class AuthConfiguration
{
    /// <summary>
    /// Adds the Authentication and Authorization to the service
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="appSettings">Application Settings</param>
    /// <param name="config"><see cref="IConfiguration"/></param>
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, AppSettings appSettings, IConfiguration config)
    {
        ConfigureJwt(services, appSettings, config);

        ConfigureAuthorization(services);

        return services;
    }

    private static void ConfigureJwt(IServiceCollection services, AppSettings appSettings, IConfiguration config)
    {
        var events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var tokenHelper = context.Request.HttpContext.RequestServices.GetRequiredService<ITokenHelper>();
                var authorization = context.Request.Headers[HeaderNames.Authorization];

                // If no authorization header found, nothing to process further
                if (string.IsNullOrEmpty(authorization))
                {
                    context.NoResult();
                    return Task.CompletedTask;
                }

                // if authorization starts with "Bearer " replace that with empty string
                context.Token = tokenHelper.DeBearerizeAuthToken(authorization);

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                logger.LogAsWarning("Authentication Failed");
                logger.LogAsError("ERR_API_AUTH_FAILED", "API Authetication failed", context.Exception);

                context.Fail(context.Exception);

                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                logger.LogAsInformation("AuthToken Validated");

                return Task.CompletedTask;
            }
        };

        var featureManager = new FeatureManager(new ConfigurationFeatureDefinitionProvider(config));

        // Enable built-in authentication of Jwt bearer token
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // using the scheme JwtBearerDefaults.AuthenticationScheme (Bearer)
            .AddJwtBearer(async authOptions => await JwtBearerConfiguration.Configure(authOptions, appSettings, events, featureManager));
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
        // amend the default policy so that
        // it checks for email and role claim
        // in addition to just an authenticated user
        var defaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Email)
            .RequireClaim(ClaimTypes.Role)
            .Build();

        // set the default policy for [Authorize] attribute
        // without a policy name
        services
            .AddAuthorizationBuilder()
            .SetDefaultPolicy(defaultPolicy);

        // add an authorization handler to handle the application access requirements
        // for a reviewer. The requirement is linked to the the custom [ApplicationAccess]
        // Authorize attribute.
        services.AddSingleton<IAuthorizationHandler, ReviewerAccessRequirementHandler>();
    }
}