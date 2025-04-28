using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IReviewBodyRepository
{
    Task<IEnumerable<ReviewBody>> GetReviewBodies(ISpecification<ReviewBody> specification);

    Task<ReviewBody> CreateReviewBody(ReviewBody reviewBody);

    Task<ReviewBody> UpdateReviewBody(ReviewBody reviewBody);

    Task<ReviewBody?> DisableReviewBody(Guid id);

    Task<ReviewBody?> EnableReviewBody(Guid id);

    Task<ReviewBodyUsers> AddUserToReviewBody(ReviewBodyUsers user);
}