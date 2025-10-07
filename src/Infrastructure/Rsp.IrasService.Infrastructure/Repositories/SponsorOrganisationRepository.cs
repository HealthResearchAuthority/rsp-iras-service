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
            if (searchQuery.RtsIds is { Count: > 0 })
            {
                query = query.Where(x =>
                    searchQuery.RtsIds.Any(rtsId =>
                        x.RtsId == rtsId));
            }

            if (searchQuery.Status != null)
            {
                query = query.Where(x => x.IsActive == searchQuery.Status.Value);
            }
        }

        return query.CountAsync();
    }
}