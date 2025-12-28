using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public class ContextAccessorService(IHttpContextAccessor contextAccessor) : IContextAccessorService
{
    public string GetUserEmail()
    {
        var claimsPrinciple = contextAccessor?.HttpContext?.User;

        return claimsPrinciple!.Claims!.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value!;
    }

    public string GetUserId()
    {
        var claimsPrinciple = contextAccessor?.HttpContext?.User;

        return claimsPrinciple!.Claims!.FirstOrDefault(x => x.Type == "userId")!.Value!;
    }
}