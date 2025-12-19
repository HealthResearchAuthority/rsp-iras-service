using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetSponsorOrganisationsSpecification : Specification<SponsorOrganisation>
{
    public GetSponsorOrganisationsSpecification(
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        SponsorOrganisationSearchRequest? searchQuery)
    {
        var builder = Query
            .AsNoTracking()
            .AsSplitQuery();

        if (searchQuery != null)
        {
            if (searchQuery.RtsIds is { Count: > 0 })
            {
                builder = builder.Where(x =>
                    searchQuery.RtsIds.Any(rtsId =>
                        x.RtsId == rtsId));
            }

            if (searchQuery.Status != null)
            {
                builder = builder.Where(x => x.IsActive == searchQuery.Status.Value);
            }

            if (searchQuery.UserId.HasValue && searchQuery.UserId.Value != Guid.Empty)
            {
                builder = builder.Where(x =>
                    x.Users.Any(u => u.UserId == searchQuery.UserId.Value));
            }
        }

        // Apply sorting
        builder = (sortField.ToLower(), sortDirection.ToLower()) switch
        {
            ("isactive", "asc") => builder.OrderByDescending(x => x.IsActive),
            ("isactive", "desc") => builder.OrderBy(x => x.IsActive),
            _ => builder.OrderBy(x => x.IsActive)
        };

        builder = builder
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}