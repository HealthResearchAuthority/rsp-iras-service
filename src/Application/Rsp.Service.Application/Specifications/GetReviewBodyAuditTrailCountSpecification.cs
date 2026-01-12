using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

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