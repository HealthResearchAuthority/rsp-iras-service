using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class SponsorOrganisationRepository(IrasContext irasContext) : ISponsorOrganisationsRepository
{
    public async Task<IEnumerable<SponsorOrganisation>> GetSponsorOrganisations(
        ISpecification<SponsorOrganisation> specification)
    {
        var result = await irasContext
            .SponsorOrganisations
            .WithSpecification(specification)
            .Include(x => x.Users)
            .AsNoTracking()
            .ToListAsync();

        return result;
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

    public async Task<SponsorOrganisation> CreateSponsorOrganisation(SponsorOrganisation sponsorOrganisation)
    {
        sponsorOrganisation.Id = Guid.NewGuid();
        sponsorOrganisation.CreatedDate = DateTime.Now;
        sponsorOrganisation.UpdatedDate = DateTime.Now;
        sponsorOrganisation.IsActive = true;

        await irasContext.SponsorOrganisations.AddAsync(sponsorOrganisation);
        await irasContext.SaveChangesAsync();
        return sponsorOrganisation;
    }

    public async Task<SponsorOrganisationUser> AddUserToSponsorOrganisation(SponsorOrganisationUser user)
    {
        var addedUser = await irasContext.SponsorOrganisationsUsers.AddAsync(user);

        var sponsorOrganisation =
            await irasContext.SponsorOrganisations.FirstOrDefaultAsync(x => x.RtsId == user.RtsId);

        if (sponsorOrganisation != null)
        {
            sponsorOrganisation.UpdatedDate = DateTime.Now;
        }

        await irasContext.SaveChangesAsync();
        return addedUser.Entity;
    }

    public async Task<SponsorOrganisationUser?> GetUserInSponsorOrganisation(string rtsId, Guid userId)
    {
        return await irasContext.SponsorOrganisationsUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RtsId == rtsId && x.UserId == userId);
    }

    public async Task<SponsorOrganisationUser> DisableUserInSponsorOrganisation(string rtsId, Guid userId)
    {
        var response = await irasContext.SponsorOrganisationsUsers
            .FirstAsync(x => x.RtsId == rtsId && x.UserId == userId);

        response.IsActive = false;

        var sponsorOrganisation = await irasContext.SponsorOrganisations.FirstOrDefaultAsync(x => x.RtsId == rtsId);

        if (sponsorOrganisation != null)
        {
            sponsorOrganisation.UpdatedDate = DateTime.Now;
        }

        await irasContext.SaveChangesAsync();
        return response;
    }

    public async Task<SponsorOrganisationUser> EnableUserInSponsorOrganisation(string rtsId, Guid userId)
    {
        var response = await irasContext.SponsorOrganisationsUsers
            .FirstAsync(x => x.RtsId == rtsId && x.UserId == userId);

        response.IsActive = true;

        var sponsorOrganisation = await irasContext.SponsorOrganisations.FirstOrDefaultAsync(x => x.RtsId == rtsId);

        if (sponsorOrganisation != null)
        {
            sponsorOrganisation.UpdatedDate = DateTime.Now;
        }

        await irasContext.SaveChangesAsync();
        return response;
    }

    public async Task<SponsorOrganisation> DisableSponsorOrganisation(string rtsId)
    {
        var sponsorOrganisation = await irasContext.SponsorOrganisations.FirstAsync(x => x.RtsId == rtsId);

        sponsorOrganisation.UpdatedDate = DateTime.Now;
        sponsorOrganisation.IsActive = false;

        await irasContext.SaveChangesAsync();

        return sponsorOrganisation;
    }

    public async Task<SponsorOrganisation> EnableSponsorOrganisation(string rtsId)
    {
        var sponsorOrganisation = await irasContext.SponsorOrganisations.FirstAsync(x => x.RtsId == rtsId);

        sponsorOrganisation.UpdatedDate = DateTime.Now;
        sponsorOrganisation.IsActive = true;

        await irasContext.SaveChangesAsync();

        return sponsorOrganisation;
    }

    public async Task<IEnumerable<SponsorOrganisationAuditTrail>> GetAuditsForSponsorOrganisation(string rtsId)
    {
        return await irasContext.SponsorOrganisationsAuditTrail
            .AsNoTracking()
            .Where(x => x.RtsId == rtsId)
            .ToListAsync();
    }
}