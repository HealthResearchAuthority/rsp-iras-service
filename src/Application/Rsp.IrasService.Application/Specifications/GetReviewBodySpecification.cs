using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetReviewBodySpecification : Specification<RegulatoryBody>
{
    public GetReviewBodySpecification(Guid id)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Users)
            .Where(rb => rb.Id == id);
    }
}