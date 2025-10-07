using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class SponsorOrganisationRepository(IrasContext irasContext) : ISponsorOrganisationsRepository
{
    public Task<IEnumerable<SponsorOrganisation>> GetSponsorOrganisations(
        ISpecification<SponsorOrganisation> specification)
    {
        var result = irasContext
            .SponsorOrganisations
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public Task<int> GetSponsorOrganisationCount(SponsorOrganisationSearchRequest searchQuery)
    {
        var query = irasContext.SponsorOrganisations
            .AsNoTracking()
            .AsQueryable();

        if (searchQuery != null)
        {
            if (!string.IsNullOrEmpty(searchQuery.SearchQuery))
            {
                var splitQuery = searchQuery.SearchQuery
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(x =>
                    splitQuery.Any(word =>
                        x.SponsorOrganisationName.Contains(word)));
            }

            if (searchQuery.Country is { Count: > 0 })
            {
                var lowerCountries = searchQuery.Country
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Select(c => c.ToLower())
                    .ToList();

                query = query.Where(rb =>
                    rb.Countries.Any(c =>
                        lowerCountries.Contains(c.ToLower())));
            }

            if (searchQuery.Status != null)
            {
                query = query.Where(x => x.IsActive == searchQuery.Status.Value);
            }
        }

        return query.CountAsync();
    }
}