using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ReviewBodyRepository(IrasContext irasContext) : IReviewBodyRepository
{
    public Task<IEnumerable<ReviewBody>> GetReviewBodies(ISpecification<ReviewBody> specification)
    {
        var result = irasContext
            .ReviewBodies
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<ReviewBody> CreateReviewBody(ReviewBody reviewBody)
    {
        reviewBody.Id = Guid.NewGuid();
        reviewBody.CreatedDate = DateTime.Now;
        reviewBody.UpdatedDate = DateTime.Now;

        await irasContext.ReviewBodies.AddAsync(reviewBody);
        await irasContext.SaveChangesAsync();
        return reviewBody;
    }

    public async Task<ReviewBody> UpdateReviewBody(ReviewBody reviewBody)
    {
        var reviewBodyEntity = await irasContext
            .ReviewBodies
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

    public async Task<ReviewBody?> DisableReviewBody(Guid id)
    {
        var reviewBodyEntity = await irasContext
            .ReviewBodies
            .SingleOrDefaultAsync(r => r.Id == id);

        if (reviewBodyEntity == null) return reviewBodyEntity;
        reviewBodyEntity.IsActive = false;
        reviewBodyEntity.UpdatedDate = DateTime.Now;
        await irasContext.SaveChangesAsync();

        return reviewBodyEntity;
    }

    public async Task<ReviewBody?> EnableReviewBody(Guid id)
    {
        var reviewBodyEntity = await irasContext
            .ReviewBodies
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

    public async Task<ReviewBodyUsers> AddUserToReviewBody(ReviewBodyUsers user)
    {
        var addedUser = await irasContext.ReviewBodyUsers.AddAsync(user);

        await irasContext.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<ReviewBodyUsers?> RemoveUserFromReviewBody(Guid reviewBodyId, Guid userId)
    {
        var userToRemove = await irasContext.ReviewBodyUsers
            .SingleOrDefaultAsync(r => r.ReviewBodyId == reviewBodyId && r.UserId == userId);

        if (userToRemove == null) return null;

        var removedUser = irasContext.ReviewBodyUsers.Remove(userToRemove);

        await irasContext.SaveChangesAsync();

        return removedUser.Entity;
    }
}