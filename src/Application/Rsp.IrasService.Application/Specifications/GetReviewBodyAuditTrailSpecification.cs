using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetReviewBodyAuditTrailSpecification : Specification<RegulatoryBodyAuditTrail>
{
    public GetReviewBodyAuditTrailSpecification(Guid id, int skip, int take)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        builder
            .Where(a => a.RegulatoryBodyId == id)
            .OrderByDescending(x => x.DateTimeStamp)
            .Skip(skip)
            .Take(take);
    }
}