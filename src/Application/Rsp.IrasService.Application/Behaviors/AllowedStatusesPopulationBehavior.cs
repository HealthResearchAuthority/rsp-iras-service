using MediatR;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.CQRS.Queries;

namespace Rsp.IrasService.Application.Behaviors;

/// <summary>
/// Populates BaseQuery.AllowedStatuses from the current HttpContext user claims.
/// Claim keys expected:
/// - "allowed_statuses/projectrecord"
/// - "allowed_statuses/modification"
/// - "allowed_statuses/document"
/// Behavior selects the most appropriate claim group based on the request path.
/// </summary>
public class AllowedStatusesPopulationBehavior<TRequest, TResponse>
(
    IHttpContextAccessor httpContextAccessor
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : BaseQuery
{
    /// <summary>
    /// Pipeline behavior entry point.
    /// - Will not override AllowedStatuses when already provided by caller.
    /// - Attempts to resolve allowed statuses from authenticated user's claims based on the request path.
    /// </summary>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // If AllowedStatuses already provided by caller, preserve it and continue.
        if (request.AllowedStatuses?.Count > 0)
        {
            return await next();
        }

        // Try to get the current HttpContext. If not available, do nothing.
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            return await next();
        }

        // Ensure we have an authenticated user principal.
        var user = httpContext.User;
        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            return await next();
        }

        // Determine which claim key to use by inspecting the request path.
        // NOTE: We inspect the HTTP request path because the pipeline does not have direct access
        // to controller/action types here. The mapping reflects endpoint URL segments to claim groups.
        var requestPath = httpContext.Request.Path.ToString();

        string claimTypeToUse = requestPath switch
        {
            // If the path contains "documents", we expect document-related allowed statuses.
            _ when requestPath.Contains("documents", StringComparison.OrdinalIgnoreCase) => "allowed_statuses/document",

            // If the path contains "projectmodifications", we expect modification-related statuses.
            _ when requestPath.Contains("projectmodifications", StringComparison.OrdinalIgnoreCase) => "allowed_statuses/modification",

            // If the path contains "applications", we expect project record-related statuses.
            _ when requestPath.Contains("applications", StringComparison.OrdinalIgnoreCase) => "allowed_statuses/projectrecord",

            // No matching claim group found for this request path.
            _ => string.Empty
        };

        // If no claim type selected, there's nothing for us to populate.
        if (string.IsNullOrEmpty(claimTypeToUse))
        {
            return await next();
        }

        // Collect claim values for the chosen claim type.
        // Steps:
        //  - Filter claims by claim type (case-insensitive).
        //  - Exclude empty values.
        //  - Split comma-separated claim values, trim entries and remove empties.
        //  - Aggregate into a list.
        var claimValues = user.Claims
            .Where(c => string.Equals(c.Type, claimTypeToUse, StringComparison.OrdinalIgnoreCase))
            .Select(c => c.Value)
            .ToList();

        // If we found any statuses in claims, assign them to the request.
        if (claimValues.Count != 0)
        {
            // Ensure AllowedStatuses is a new list instance to avoid accidental shared references.
            request.AllowedStatuses = claimValues;
        }

        // Continue pipeline execution.
        return await next();
    }
}