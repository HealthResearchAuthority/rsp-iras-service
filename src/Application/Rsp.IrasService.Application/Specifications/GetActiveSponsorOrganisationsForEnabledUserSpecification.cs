using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetActiveSponsorOrganisationsForEnabledUserSpecification : Specification<SponsorOrganisation>
{
    public GetActiveSponsorOrganisationsForEnabledUserSpecification(Guid userId)
    {
        Query
            .Include(org => org.Users)
            .Where(org => org.IsActive && org.Users.Any(u => u.UserId == userId && u.IsActive));
    }
}