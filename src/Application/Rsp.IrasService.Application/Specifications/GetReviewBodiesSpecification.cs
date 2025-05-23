using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetReviewBodiesSpecification : Specification<ReviewBody>
{
    public GetReviewBodiesSpecification(int pageNumber, int pageSize, string? searchQuery)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            var splitQuery = searchQuery.Split(' ');

            builder = builder.Where(x =>
                        splitQuery.Any(word =>
                            x.OrganisationName.Contains(word)
                            ));
        }

        builder.OrderBy(x => x.OrganisationName)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
    }
}