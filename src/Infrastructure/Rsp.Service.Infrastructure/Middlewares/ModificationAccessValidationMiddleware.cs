using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;

namespace Rsp.Service.Infrastructure.Middlewares;

public class ModificationAccessValidationMiddleware(IAccessValidationRepository accessValidationRepository) : IMiddleware
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

        // Only run validation for endpoints that include modificationId in query or route

        // Check querystring first

        var query = context.Request.Query;

        string? projectRecordId = null;
        Guid modificationId = Guid.Empty;
        Guid modificationChangeId = Guid.Empty;

        var keys = new[] { "projectRecordId", "modificationId", "projectModificationId", "modificationChangeId" };

        foreach (var key in keys)
        {
            if (query.TryGetValue(key, out var valueFromQuery))
            {
                _ = key switch
                {
                    "projectRecordId" => AssignProjectRecordId(valueFromQuery, ref projectRecordId),
                    "modificationId" or "projectModificationId" => AssignModificationId(valueFromQuery, ref modificationId),
                    "modificationChangeId" => AssignModificationChangeId(valueFromQuery, ref modificationChangeId),
                    _ => false,
                };
            }
            else if (context.Request.RouteValues.TryGetValue(key, out var valueFromRoute))
            {
                var stringValue = valueFromRoute?.ToString();

                _ = key switch
                {
                    "projectRecordId" => AssignProjectRecordId(stringValue, ref projectRecordId),
                    "modificationId" or "projectModificationId" => AssignModificationId(stringValue, ref modificationId),
                    "modificationChangeId" => AssignModificationChangeId(stringValue, ref modificationChangeId),
                    _ => false
                };
            }
        }

        // If neither ids are present, skip validation
        if (string.IsNullOrWhiteSpace(projectRecordId) && modificationId == Guid.Empty && modificationChangeId == Guid.Empty)
        {
            await next(context);
            return;
        }

        // If only modificationChangeId is provided, get modificationId from it
        if (string.IsNullOrWhiteSpace(projectRecordId) && modificationId == Guid.Empty && modificationChangeId != Guid.Empty)
        {
            if (await accessValidationRepository.HasModificationAccess(userId, modificationChangeId))
            {
                await next(context);
                return;
            }
        }

        // Call repository to check access
        var hasAccess = await accessValidationRepository.HasModificationAccess(userId, projectRecordId, modificationId);

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

    private static bool AssignModificationId(string? value, ref Guid modificationId)
    {
        if (Guid.TryParse(value, out var guid))
        {
            modificationId = guid;
        }

        return true;
    }

    private static bool AssignModificationChangeId(string? value, ref Guid modificationChangeId)
    {
        if (Guid.TryParse(value, out var guid))
        {
            modificationChangeId = guid;
        }

        return true;
    }
}