using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Contracts.Repositories;

namespace Rsp.IrasService.Infrastructure.Middlewares;

public class DocumentAccessValidationMiddleware(IAccessValidationRepository accessValidationRepository) : IMiddleware
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

        Guid documentId = Guid.Empty;
        Guid modificationId = Guid.Empty;

        var keys = new[] { "documentId", "modificationId" };

        foreach (var key in keys)
        {
            if (query.TryGetValue(key, out var valueFromQuery))
            {
                _ = key switch
                {
                    "documentId" => AssignDocumentId(valueFromQuery, ref documentId),
                    "modificationId" => AssignModificationId(valueFromQuery, ref modificationId),
                    _ => false,
                };
            }
            else if (context.Request.RouteValues.TryGetValue(key, out var valueFromRoute))
            {
                var stringValue = valueFromRoute?.ToString();

                _ = key switch
                {
                    "documentId" => AssignDocumentId(stringValue, ref documentId),
                    "modificationId" => AssignModificationId(stringValue, ref modificationId),
                    _ => false
                };
            }
        }

        // If neither ids are present, skip validation
        if (documentId == Guid.Empty && modificationId == Guid.Empty)
        {
            await next(context);
            return;
        }

        var hasAccess = false;

        if (documentId != Guid.Empty)
        {
            hasAccess = await accessValidationRepository.HasDocumentAccess(userId, documentId);
        }

        if (modificationId != Guid.Empty)
        {
            hasAccess = await accessValidationRepository.HasModificationAccess(userId, null, modificationId);
        }

        if (!hasAccess)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await next(context);
    }

    private static bool AssignDocumentId(string? value, ref Guid documentId)
    {
        if (Guid.TryParse(value, out var guid))
        {
            documentId = guid;
        }

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
}