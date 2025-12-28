using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ContextAccessorService(IHttpContextAccessor contextAccessor) : IContextAccessorService
{
    public string GetUserEmail()
    {
        var claimsPrincipal = contextAccessor?.HttpContext?.User;

        var email = claimsPrincipal?
            .Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.Email)?
            .Value;

        return string.IsNullOrWhiteSpace(email)
            ? "System generated"
            : email;
    }

    public string GetUserId()
    {
        var claimsPrinciple = contextAccessor?.HttpContext?.User;

        return claimsPrinciple?.Claims?.FirstOrDefault(x => x.Type == "userId")?.Value ?? "System generated";
    }
}