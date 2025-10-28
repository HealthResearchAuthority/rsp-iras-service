using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Rsp.IrasService.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class AuditTrailDetailsService(IHttpContextAccessor contextAccessor) : IAuditTrailDetailsService
{
    public string GetEmailFromHttpContext()
    {
        return "dan.hulmston@paconsulting.com";

        var claimsPrinciple = contextAccessor?.HttpContext?.User;

        return claimsPrinciple!.Claims!.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value!;
    }
}