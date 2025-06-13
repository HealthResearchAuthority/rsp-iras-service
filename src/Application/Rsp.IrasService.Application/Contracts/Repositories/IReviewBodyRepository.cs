using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IReviewBodyRepository
{
    Task<IEnumerable<RegulatoryBody>> GetReviewBodies(ISpecification<RegulatoryBody> specification);

    Task<RegulatoryBody?> GetReviewBody(ISpecification<RegulatoryBody> specification);

    Task<RegulatoryBody> CreateReviewBody(RegulatoryBody reviewBody);

    Task<RegulatoryBody> UpdateReviewBody(RegulatoryBody reviewBody);

    Task<RegulatoryBody?> DisableReviewBody(Guid id);

    Task<RegulatoryBody?> EnableReviewBody(Guid id);

    Task<RegulatoryBodyUsers> AddUserToReviewBody(RegulatoryBodyUsers user);

    Task<RegulatoryBodyUsers?> RemoveUserFromReviewBody(Guid reviewBodyId, Guid userId);

    Task<int> GetReviewBodyCount(string? searchQuery = null);
}