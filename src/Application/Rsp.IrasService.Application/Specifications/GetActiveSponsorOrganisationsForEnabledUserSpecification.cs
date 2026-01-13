using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetActiveSponsorOrganisationsForEnabledUserSpecification : Specification<SponsorOrganisation>
{
    public GetActiveSponsorOrganisationsForEnabledUserSpecification(Guid userId)
    {
        Query
            .Include(org => org.Users)
            .Where(org => org.IsActive && org.Users.Any(u => u.UserId == userId && u.IsActive));
    }
}