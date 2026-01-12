using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;

namespace Rsp.Service.Infrastructure.Middlewares;

public class ProjectRecordAccessValidationMiddleware(IAccessValidationRepository accessValidationRepository) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var user = context.User;

        var userId = user.FindFirst("userId")?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            // if no user id, let authorization handle
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        // Only run validation for endpoints that include projectRecordId or applicationId in query or route

        // Check querystring first

        var query = context.Request.Query;

        string? projectRecordId = null;

        var keys = new[] { "projectRecordId", "applicationId" };

        foreach (var key in keys)
        {
            if (query.TryGetValue(key, out var valueFromQuery))
            {
                _ = key switch
                {
                    "projectRecordId" or "applicationId" => AssignProjectRecordId(valueFromQuery, ref projectRecordId),
                    _ => false,
                };
            }
            else if (context.Request.RouteValues.TryGetValue(key, out var valueFromRoute))
            {
                var stringValue = valueFromRoute?.ToString();

                _ = key switch
                {
                    "projectRecordId" or "applicationId" => AssignProjectRecordId(stringValue, ref projectRecordId),
                    _ => false
                };
            }
        }

        // If neither ids are present, skip validation
        if (string.IsNullOrWhiteSpace(projectRecordId))
        {
            await next(context);
            return;
        }

        // Call repository to check access
        var hasAccess = await accessValidationRepository.HasProjectAccess(userId, projectRecordId);

        if (!hasAccess)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await next(context);
    }

    private static bool AssignProjectRecordId(string? value, ref string? projectRecordId)
    {
        projectRecordId = value;

        return true;
    }
}