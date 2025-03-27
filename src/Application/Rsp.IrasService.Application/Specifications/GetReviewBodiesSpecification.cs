using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetReviewBodySpecification : Specification<ReviewBody>
{
    public GetReviewBodySpecification(Guid? id = null)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        if (id != null)
        {
            builder.Where(rb => rb.Id == id);
        }
    }
}