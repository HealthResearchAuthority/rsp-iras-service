using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IReviewBodyAuditTrailRepository
{
    IEnumerable<ReviewBodyAuditTrail> GetForReviewBody(ISpecification<ReviewBodyAuditTrail> specification);

    Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<ReviewBodyAuditTrail> specification);

    public Task<IEnumerable<ReviewBodyAuditTrail>> CreateAuditRecords(IEnumerable<ReviewBodyAuditTrail> reviewBodyAuditTrail);
}