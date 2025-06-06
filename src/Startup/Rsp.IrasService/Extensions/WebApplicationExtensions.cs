﻿using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Infrastructure;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Extensions;

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

            await using var context = scope.ServiceProvider.GetRequiredService<IrasContext>();

            await context.Database.MigrateAsync();

            logger.LogAsInformation("Migrations Completed");
        }
        catch (Exception ex)
        {
            logger.LogAsError("ERR_FAILED_MIGRATIONS", "Database Migration failed", ex);
        }
    }
}