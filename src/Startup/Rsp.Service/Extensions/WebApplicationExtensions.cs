using Microsoft.EntityFrameworkCore;
using Rsp.Service.Domain.Constants;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Middlewares;
using Rsp.Logging.Extensions;

namespace Rsp.Service.Extensions;

/// <summary>
/// Define an extension method on WebApplication to support migrating and seeding the database
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Migrates and seed the database.
    /// </summary>
    /// <param name="app">The WebApplication instance</param>
    public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Performing Migrations");

            using var scope = app.Services.CreateScope();

            await using var context = scope.ServiceProvider.GetRequiredService<RspContext>();

            await context.Database.MigrateAsync();

            logger.LogAsInformation("Migrations Completed");
        }
        catch (Exception ex)
        {
            logger.LogAsError("ERR_FAILED_MIGRATIONS", "Database Migration failed", ex);
        }
    }

    /// <summary>
    /// Adds access validation middleware to the application pipeline.
    /// </summary>
    /// <param name="app">
    /// The web application builder.
    /// </param>
    public static IApplicationBuilder UseAccessValidation(this WebApplication app)
    {
        return app.UseWhen
        (
            context =>
            {
                // If no user, allow pipeline to handle auth
                var user = context.User;

                return user.Identity?.IsAuthenticated is true &&
                       !user.IsInRole(Roles.SystemAdministrator) &&
                        // check for sponsor and applicant
                        // high-privilege roles will be by passed
                        (
                            user.IsInRole(Roles.Applicant) ||
                            user.IsInRole(Roles.Sponsor)
                        );
            }, appBuilder =>
            {
                // verify project record access
                appBuilder.UseWhen
                (
                    context =>
                    {
                        var path = context.Request.Path;

                        return path.StartsWithSegments("/applications");
                    },
                    appBuilder => appBuilder.UseProjectRecordAccessValidation()
                );

                // verify document access
                appBuilder.UseWhen
                (
                    context =>
                    {
                        var path = context.Request.Path;

                        return path.StartsWithSegments("/documents") ||
                               path.StartsWithSegments("/respondent/modificationdocumentdetails") ||
                               path.StartsWithSegments("/respondent/modificationdocumentanswer");
                    },

                    appBuilder => appBuilder.UseDocumentAccessValidation()
                );

                // verify modification access
                appBuilder.UseWhen
                (
                    context =>
                    {
                        var path = context.Request.Path;
                        return path.StartsWithSegments("/projectmodifications") ||
                               path.StartsWithSegments("/respondent/modification") ||
                               path.StartsWithSegments("/respondent/modificationchange");
                    },

                    appBuilder => appBuilder.UseModificationAccessValidation()
                );
            }
        );
    }
}