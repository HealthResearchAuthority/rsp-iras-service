using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetReviewBodyAuditTrailSpecification : Specification<RegulatoryBodyAuditTrial>
{
    public GetReviewBodyAuditTrailSpecification(Guid id, int skip, int take)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        builder
            .Where(a => a.RegulatoryBodiesId == id)
            .OrderByDescending(x => x.DateTimeStamp)
            .Skip(skip)
            .Take(take);
    }
}