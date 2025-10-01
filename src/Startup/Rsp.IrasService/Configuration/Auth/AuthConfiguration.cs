﻿using System.IdentityModel.Tokens.Jwt;
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
        //Check if there is a 
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
            .AddAuthentication()
            // using the scheme JwtBearerDefaults.AuthenticationScheme (Bearer)
            .AddJwtBearer("DefaultBearer", async authOptions => await JwtBearerConfiguration.Configure(authOptions, appSettings, events, featureManager))
            .AddJwtBearer("FunctionAppBearer", options =>
            {
                options.Authority = appSettings.MicrosoftEntraSettings.Authority;
                options.Audience = appSettings.MicrosoftEntraSettings.IrasServiceAPIID; 
                options.Events = events;
            })
            .AddPolicyScheme("dynamicBearer", null, options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var tokenHelper = context.Request.HttpContext.RequestServices.GetRequiredService<ITokenHelper>();
                        var authToken = context.Request.Headers[HeaderNames.Authorization];

                        // if we don't have token, there is nothing to forward to
                        if (string.IsNullOrWhiteSpace(authToken))
                        {
                            return JwtBearerDefaults.AuthenticationScheme;
                        }

                        // replace the "Bearer " if present in the token
                        var token = tokenHelper.DeBearerizeAuthToken(authToken);
                        var jwtHandler = new JwtSecurityTokenHandler();

                        // if we can't read the token, return the empty scheme
                        if (!jwtHandler.CanReadToken(token))
                        {
                            return JwtBearerDefaults.AuthenticationScheme;
                        }

                        // get the token to verify the issuer
                        var jwtSecurityToken = jwtHandler.ReadJwtToken(token);

                    // based on the issuer, we will forward the request to the appropriate scheme
                    // if the token issuer is the one for OneLogin, use the default JwtBearer scheme
                    // if the issuer is the one for Microsoft Entra ID, use the FunctionAppBearer scheme
                    return jwtSecurityToken.Issuer == appSettings.MicrosoftEntraSettings.Authority ? "FunctionAppBearer" : "DefaultBearer";
                };
            });
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
        // amend the default policy so that
        // it checks for email and role claim
        // in addition to just an authenticated user
        //var defaultPolicy = new AuthorizationPolicyBuilder()
        //    .RequireAuthenticatedUser()
        //    .RequireClaim(ClaimTypes.Email)
        //    .RequireClaim(ClaimTypes.Role)
        //    .Build();

        //// set the default policy for [Authorize] attribute
        //// without a policy name
        //services
        //    .AddAuthorizationBuilder()
        //    .SetDefaultPolicy(defaultPolicy);


        services.AddAuthorization(options =>
        {
            // Policy that only uses defaultBearer scheme
            options.AddPolicy("UseDefaultBearerOnly", policy =>
            {
                policy
                    .AddAuthenticationSchemes("defaultBearer")
                    .RequireAuthenticatedUser()
                    .RequireClaim(ClaimTypes.Email)
                    .RequireClaim(ClaimTypes.Role);
            });

            // Optional: default policy can use dynamicBearer if you want
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes("dynamicBearer")
                .RequireAuthenticatedUser()
                .Build();
        });

        // add an authorization handler to handle the application access requirements
        // for a reviewer. The requirement is linked to the the custom [ApplicationAccess]
        // Authorize attribute.
        services.AddSingleton<IAuthorizationHandler, ReviewerAccessRequirementHandler>();
    }
}