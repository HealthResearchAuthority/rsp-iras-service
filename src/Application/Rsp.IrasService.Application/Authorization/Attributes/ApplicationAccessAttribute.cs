using Microsoft.AspNetCore.Authorization;
using Rsp.IrasService.Application.Authorization.Requirements;

namespace Rsp.IrasService.Application.Authorization.Attributes;

/// <summary>
/// Custom Authorization Attribute with links to authorization requirements.
/// Each authorization requirement is associated with an AuthorizationHandler for
/// the requirement. Handlers are mutally exclusive, if one succeed, we ignore the other ones
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApplicationAccessAttribute() : AuthorizeAttribute, IAuthorizationRequirementData
{
    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        return
        [
            /// this requirement is linked to the <see cref="ReviewerAccessRequirementHandler"/>
            new ReviewerAccessRequirement()
        ];
    }
}