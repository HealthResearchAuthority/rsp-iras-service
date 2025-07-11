using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetReviewBodiesSpecification : Specification<RegulatoryBody>
{
    public GetReviewBodiesSpecification(int pageNumber, int pageSize, ReviewBodySearchRequest? searchQuery)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        if (searchQuery != null)
        {
            if (!string.IsNullOrEmpty(searchQuery.SearchQuery))
            {
                var splitQuery = searchQuery.SearchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                builder = builder.Where(x =>
                    splitQuery.Any(word =>
                        x.RegulatoryBodyName.Contains(word)
                        ));
            }

            if (searchQuery.Country is { Count: > 0 })
            {
                var lowerCountries = searchQuery.Country
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Select(c => c.ToLower())
                    .ToList();

                builder = builder.Where(rb =>
                    rb.Countries.Any(c =>
                        lowerCountries.Contains(c.ToLower())));
            }

            if (searchQuery.Status != null)
            {
                builder.Where(x => x.IsActive == searchQuery.Status.Value);
            }
        }

        builder.OrderBy(x => x.RegulatoryBodyName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}