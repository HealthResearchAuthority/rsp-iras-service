using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetReviewBodyAuditTrailCountSpecification : Specification<RegulatoryBodyAuditTrail>
{
    public GetReviewBodyAuditTrailCountSpecification(Guid id)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        builder
            .Where(a => a.RegulatoryBodyId == id);
    }
}