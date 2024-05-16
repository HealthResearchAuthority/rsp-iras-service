using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Rsp.IrasService.Application.AuthenticationHelpers;
using Rsp.IrasService.Application.Settings;

namespace Rsp.IrasService.Configuration.Auth;

/// <summary>
/// Authentication and Authorization configuration
/// </summary>
[ExcludeFromCodeCoverage]
public static class AuthConfiguration
{
    /// <summary>
    /// Adds the Authentication and Authorization to the service
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="appSettings">Application Settinghs</param>
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, AppSettings appSettings)
    {
        ConfigureJwt(services, appSettings);

        ConfigureAuthorization(services, []);

        return services;
    }

    private static void ConfigureJwt(IServiceCollection services, AppSettings appSettings)
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
                // TODO: Add logging when authentication fails?
                context.Fail(context.Exception);

                return Task.CompletedTask;
            },
            OnTokenValidated = _ => Task.CompletedTask
        };

        // Enable built-in authentication of Jwt bearer token
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // using the scheme JwtBearerDefaults.AuthenticationScheme (Bearer)
            .AddJwtBearer(authOptions => JwtBearerConfiguration.Configure(authOptions, appSettings, events));
    }

    private static void ConfigureAuthorization(IServiceCollection services, List<string> roles)
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Email)
            .RequireClaim(ClaimTypes.NameIdentifier)
            .Build();

        services
            .AddAuthorizationBuilder()
            .SetDefaultPolicy(policy);
    }
}