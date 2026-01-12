using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetSponsorOrganisationAuditTrailSpecification : Specification<SponsorOrganisationAuditTrail>
{
    public GetSponsorOrganisationAuditTrailSpecification(
        string id)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        builder
            .Where(a => a.RtsId == id)
            .OrderByDescending(x => x.DateTimeStamp);
    }
}