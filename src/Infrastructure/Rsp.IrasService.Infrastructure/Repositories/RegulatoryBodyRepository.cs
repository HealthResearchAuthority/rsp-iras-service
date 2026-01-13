using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

public class RegulatoryBodyRepository(IrasContext irasContext) : IRegulatoryBodyRepository
{
    public Task<IEnumerable<RegulatoryBody>> GetRegulatoryBodies(ISpecification<RegulatoryBody> specification)
    {
        var result = irasContext
            .RegulatoryBodies
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<RegulatoryBody> CreateRegulatoryBody(RegulatoryBody reviewBody)
    {
        reviewBody.Id = Guid.NewGuid();
        reviewBody.CreatedDate = DateTime.Now;

        await irasContext.RegulatoryBodies.AddAsync(reviewBody);
        await irasContext.SaveChangesAsync();
        return reviewBody;
    }

    public async Task<RegulatoryBody> UpdateRegulatoryBody(RegulatoryBody reviewBody)
    {
        var reviewBodyEntity = await irasContext
            .RegulatoryBodies
            .SingleOrDefaultAsync(r => r.Id == reviewBody.Id);

        if (reviewBodyEntity == null) return await CreateRegulatoryBody(reviewBody);

        irasContext.Entry(reviewBodyEntity).CurrentValues.SetValues(reviewBody);
        irasContext.Entry(reviewBodyEntity).Property(r => r.Id).IsModified = false;
        irasContext.Entry(reviewBodyEntity).Property(r => r.CreatedDate).IsModified = false;
        irasContext.Entry(reviewBodyEntity).Property(r => r.CreatedBy).IsModified = false;
        irasContext.Entry(reviewBodyEntity).Property(r => r.IsActive).IsModified = false;

        reviewBodyEntity.UpdatedDate = DateTime.Now;

        await irasContext.SaveChangesAsync();
        return reviewBodyEntity;
    }

    public async Task<RegulatoryBody?> DisableRegulatoryBody(Guid id)
    {
        var reviewBodyEntity = await irasContext
            .RegulatoryBodies
            .SingleOrDefaultAsync(r => r.Id == id);

        if (reviewBodyEntity == null) return reviewBodyEntity;
        reviewBodyEntity.IsActive = false;
        reviewBodyEntity.UpdatedDate = DateTime.Now;
        await irasContext.SaveChangesAsync();

        return reviewBodyEntity;
    }

    public async Task<RegulatoryBody?> EnableRegulatoryBody(Guid id)
    {
        var reviewBodyEntity = await irasContext
            .RegulatoryBodies
            .SingleOrDefaultAsync(r => r.Id == id);

        if (reviewBodyEntity == null)
        {
            return reviewBodyEntity;
        }
        reviewBodyEntity.IsActive = true;
        reviewBodyEntity.UpdatedDate = DateTime.Now;
        await irasContext.SaveChangesAsync();

        return reviewBodyEntity;
    }

    public async Task<RegulatoryBodyUser> AddUserToRegulatoryBody(RegulatoryBodyUser user)
    {
        var addedUser = await irasContext.RegulatoryBodiesUsers.AddAsync(user);

        await irasContext.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<RegulatoryBodyUser?> RemoveUserFromRegulatoryBody(Guid reviewBodyId, Guid userId)
    {
        var userToRemove = await irasContext.RegulatoryBodiesUsers
            .SingleOrDefaultAsync(r => r.Id == reviewBodyId && r.UserId == userId);

        if (userToRemove == null) return null;

        var removedUser = irasContext.RegulatoryBodiesUsers.Remove(userToRemove);

        await irasContext.SaveChangesAsync();

        return removedUser.Entity;
    }

    public async Task<RegulatoryBody?> GetRegulatoryBody(ISpecification<RegulatoryBody> specification)
    {
        var result = await irasContext
           .RegulatoryBodies
           .WithSpecification(specification)
           .FirstOrDefaultAsync();

        return result;
    }

    public Task<int> GetRegulatoryBodyCount(ReviewBodySearchRequest? searchQuery)
    {
        var query = irasContext.RegulatoryBodies
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
                        x.RegulatoryBodyName.Contains(word)));
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

    public async Task<List<RegulatoryBodyUser>> GetRegulatoryBodiesUsersByUserId(Guid userId)
    {
        return await irasContext.RegulatoryBodiesUsers
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<RegulatoryBodyUser>> GetRegulatoryBodiesUsersByIds(List<Guid> ids)
    {
        return await irasContext.RegulatoryBodiesUsers
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}