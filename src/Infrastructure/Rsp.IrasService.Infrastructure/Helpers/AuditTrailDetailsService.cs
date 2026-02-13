using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class AuditTrailDetailsService(IHttpContextAccessor contextAccessor) : IAuditTrailDetailsService
{
    public string GetEmailFromHttpContext()
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
}