using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Constants;

namespace Rsp.IrasService.Infrastructure.Middlewares;

public class AccessValidationMiddleware(IAccessValidationRepository accessValidationRepository) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // If no user, allow pipeline to handle auth
        var user = context.User;
        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            await next(context);
            return;
        }

        // Bypass for high-privilege roles
        if
        (
            user.IsInRole(Roles.TeamManager) ||
            user.IsInRole(Roles.WorkflowCoordinator) ||
            user.IsInRole(Roles.SystemAdministrator) ||
            user.IsInRole(Roles.StudyWideReviewer)
        )
        {
            await next(context);
            return;
        }

        // Extract user id from claims (sub or nameidentifier)
        var userId = user.FindFirst("userId")?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            // if no user id, let authorization handle
            await next(context);
            return;
        }

        // Only run validation for endpoints that include project or modification id in query or route
        // Check querystring first
        var query = context.Request.Query;
        string? projectRecordId = null;
        Guid? modificationId = null;

        if (query.TryGetValue("projectRecordId", out var projectRecordIdViaQuery))
        {
            projectRecordId = projectRecordIdViaQuery;
        }
        else if (query.TryGetValue("applicationId", out var applicationIdViaQuery))
        {
            projectRecordId = applicationIdViaQuery;
        }
        else if (context.Request.RouteValues.TryGetValue("projectRecordId", out var projectRecordIdViaRoute))
        {
            projectRecordId = projectRecordIdViaRoute?.ToString();
        }
        else if (context.Request.RouteValues.TryGetValue("applicationId", out var applicationIdViaRoute))
        {
            projectRecordId = applicationIdViaRoute?.ToString();
        }

        if (query.TryGetValue("modificationId", out var modificationIdViaQuery))
        {
            if (Guid.TryParse(modificationIdViaQuery, out var modificationGuid))
            {
                modificationId = modificationGuid;
            }
        }
        else if (query.TryGetValue("projectModificationId", out var projectModificationIdViaQuery))
        {
            if (Guid.TryParse(projectModificationIdViaQuery, out var projectModificationGuid))
            {
                modificationId = projectModificationGuid;
            }
        }
        else if (context.Request.RouteValues.TryGetValue("modificationId", out var modificationIdViaRoute))
        {
            if (Guid.TryParse(modificationIdViaRoute?.ToString(), out var modificationGuid))
            {
                modificationId = modificationGuid;
            }
        }
        else if (context.Request.RouteValues.TryGetValue("projectModificationId", out var projectModificationIdViaRoute))
        {
            if (Guid.TryParse(projectModificationIdViaRoute?.ToString(), out var projectModificationGuid))
            {
                modificationId = projectModificationGuid;
            }
        }

        // If neither ids are present, skip validation
        if (string.IsNullOrEmpty(projectRecordId) && modificationId == null)
        {
            await next(context);
            return;
        }

        // Determine roles that require validation: applicant, sponsor, study-wide_reviewer
        var requiresValidation = user.IsInRole(Roles.Applicant) || user.IsInRole(Roles.Sponsor);

        if (!requiresValidation)
        {
            // Not a role we validate here => allow
            await next(context);
            return;
        }

        // Call repository to check access
        var hasAccess = await accessValidationRepository.HasAccessAsync(userId, projectRecordId, modificationId);

        if (!hasAccess)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //await context.Response.WriteAsync("Forbidden");
            return;
        }

        await next(context);
    }
}

/// <summary>
/// This middleware will validate if the user has access to the requested resource
/// by checking their roles and see if they have access to the project, modification or document.
/// </summary>
[ExcludeFromCodeCoverage]
public static class AccessValidationExtensions
{
    /// <summary>
    /// Adds <see cref="AccessValidationMiddleware"/> middleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseAccessValidation(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        return app.UseMiddleware<AccessValidationMiddleware>();
    }
}