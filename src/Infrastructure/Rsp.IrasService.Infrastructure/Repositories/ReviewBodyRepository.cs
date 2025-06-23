using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ReviewBodyRepository(IrasContext irasContext) : IRegulatoryBodyRepository
{
    public Task<IEnumerable<RegulatoryBody>> GetReviewBodies(ISpecification<RegulatoryBody> specification)
    {
        var result = irasContext
            .RegulatoryBodies
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<RegulatoryBody> CreateReviewBody(RegulatoryBody reviewBody)
    {
        reviewBody.Id = Guid.NewGuid();
        reviewBody.CreatedDate = DateTime.Now;

        await irasContext.RegulatoryBodies.AddAsync(reviewBody);
        await irasContext.SaveChangesAsync();
        return reviewBody;
    }

    public async Task<RegulatoryBody> UpdateReviewBody(RegulatoryBody reviewBody)
    {
        var reviewBodyEntity = await irasContext
            .RegulatoryBodies
            .SingleOrDefaultAsync(r => r.Id == reviewBody.Id);

        if (reviewBodyEntity == null) return await CreateReviewBody(reviewBody);

        irasContext.Entry(reviewBodyEntity).CurrentValues.SetValues(reviewBody);
        irasContext.Entry(reviewBodyEntity).Property(r => r.Id).IsModified = false;
        irasContext.Entry(reviewBodyEntity).Property(r => r.CreatedDate).IsModified = false;
        irasContext.Entry(reviewBodyEntity).Property(r => r.CreatedBy).IsModified = false;
        irasContext.Entry(reviewBodyEntity).Property(r => r.IsActive).IsModified = false;

        reviewBodyEntity.UpdatedDate = DateTime.Now;

        await irasContext.SaveChangesAsync();
        return reviewBodyEntity;
    }

    public async Task<RegulatoryBody?> DisableReviewBody(Guid id)
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

    public async Task<RegulatoryBody?> EnableReviewBody(Guid id)
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

    public async Task<RegulatoryBodyUsers> AddUserToReviewBody(RegulatoryBodyUsers user)
    {
        var addedUser = await irasContext.RegulatoryBodyUsers.AddAsync(user);

        await irasContext.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<RegulatoryBodyUsers?> RemoveUserFromReviewBody(Guid reviewBodyId, Guid userId)
    {
        var userToRemove = await irasContext.RegulatoryBodyUsers
            .SingleOrDefaultAsync(r => r.Id == reviewBodyId && r.UserId == userId);

        if (userToRemove == null) return null;

        var removedUser = irasContext.RegulatoryBodyUsers.Remove(userToRemove);

        await irasContext.SaveChangesAsync();

        return removedUser.Entity;
    }

    public async Task<RegulatoryBody?> GetReviewBody(ISpecification<RegulatoryBody> specification)
    {
        var result = await irasContext
           .RegulatoryBodies
           .WithSpecification(specification)
           .FirstOrDefaultAsync();

        return result;
    }

    public Task<int> GetReviewBodyCount(string? searchQuery = null)
    {
        if (!string.IsNullOrEmpty(searchQuery))
        {
            var splitQuery = searchQuery.Split(' ');

            return irasContext.RegulatoryBodies.CountAsync(x =>
                        splitQuery.Any(word =>
                            x.RegulatoryBodyName.Contains(word)
                            ));
        }

        return irasContext.RegulatoryBodies.CountAsync();
    }
}