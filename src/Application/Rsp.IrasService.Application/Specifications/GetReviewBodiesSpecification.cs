using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetReviewBodiesSpecification : Specification<RegulatoryBody>
{
    public GetReviewBodiesSpecification(int pageNumber, int pageSize, string sortField, string sortDirection ,ReviewBodySearchRequest? searchQuery)
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

        // Apply sorting
        builder = (sortField.ToLower(), sortDirection.ToLower()) switch
        {
            ("regulatorybodyname", "asc") => builder.OrderBy(x => x.RegulatoryBodyName).ThenBy(x => x.IsActive),
            ("regulatorybodyname", "desc") => builder.OrderByDescending(x => x.RegulatoryBodyName).ThenBy(x => x.IsActive),
            ("countries", "asc") => builder.OrderBy(x => x.Countries.FirstOrDefault()).ThenBy(x => x.IsActive),
            ("countries", "desc") => builder.OrderByDescending(x => x.Countries.FirstOrDefault()).ThenBy(x => x.IsActive),

            // Sort so that active records (IsActive == true) appear first when sorting ascending
            ("isactive", "asc") => builder.OrderByDescending(x => x.IsActive),

            // Sort so that inactive records (IsActive == false) appear first when sorting descending
            ("isactive", "desc") => builder.OrderBy(x => x.IsActive),

            _ => builder.OrderBy(x => x.RegulatoryBodyName).ThenByDescending(x => x.IsActive)
        };

        builder
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}