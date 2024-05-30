using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Authorization.Requirements;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Application.Authorization.Handlers;

/// <summary>
/// Authorization handler to validate if user is a reviewer and can query the statuses
/// </summary>
public class ReviewerAccessRequirementHandler(ILogger<ReviewerAccessRequirementHandler> logger) : AuthorizationHandler<ReviewerAccessRequirement>
{
    /// <summary>
    /// Handles authorization requirement for ensuring a user is indeed a reviewer
    /// and can only query certain application statuses
    /// </summary>
    /// <param name="context">Authorization handler context</param>
    /// <param name="requirement">The requirement being handled by this handler. <see cref="ReviewerAccessRequirement"/></param>
    /// <returns>A task. Implementation marks the context as either fail or success</returns>
    /// <remarks>
    /// As per docs, Authorization handlers are called even if authentication fails. So always handle scenario in authorization handler when the
    /// user is not authenticated.
    /// </remarks>
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ReviewerAccessRequirement requirement)
    {
        var pendingRequirements = context.PendingRequirements;

        // Multiple requirements will be linked to the ApplicationAccess AuthorizeAttribute and will work
        // in a mutually exclusive manner. This means that if another requirement is met already by a handler
        // that was executed earlier than this then we don't need to process this requirement
        if (pendingRequirements.SingleOrDefault(pendingRequirement => pendingRequirement == requirement) == null)
        {
            return;
        }

        // if the context has failed
        // the common authorization requirement must not be met
        // user should have email and role claim
        if (context.HasFailed)
        {
            return;
        }

        var roleClaims = context.User.FindAll(ClaimTypes.Role).ToList();

        // user should be in the reviewer role
        if (roleClaims.Find(claim => claim.Value == ReviewerAccessRequirement.Role) == null)
        {
            logger.LogErrorHp(string.Join(",", roleClaims), "ERR_AUTH_FAILED", "user is not in the required role");

            // Do not fail the requirement as the handler is meant to work as OR
            // so the next handler will pick the next requirement, uncomment if AND behaviour is intended
            // context.Fail(new AuthorizationFailureReason(this, "user is not in the required role"))

            return;
        }

        if (context.Resource is not HttpContext httpContext)
        {
            return;
        }

        // this list should come from database hardcoded for now
        // TODOI: change this to fetch the statuses, reviewer can see, from database
        requirement.AllowedStatuses = ["pending"];

        var routeValues = httpContext.GetRouteData().Values;

        // can't find the status in the query or route
        // or it's not one of the required statuses
        if (routeValues["status"] is not string status ||
            requirement.AllowedStatuses.FirstOrDefault(required => required == status) == null)
        {
            logger.LogErrorHp(string.Join(",", roleClaims), "ERR_AUTH_FAILED", "user is not allowed to query the status");

            return;
        }

        context.Succeed(requirement);

        logger.LogInformationHp(nameof(ReviewerAccessRequirement) + " was met successfully");

        await Task.CompletedTask;
    }
}