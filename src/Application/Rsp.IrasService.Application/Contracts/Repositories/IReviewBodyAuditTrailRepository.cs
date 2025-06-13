using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IReviewBodyAuditTrailRepository
{
    IEnumerable<RegulatoryBodyAuditTrial> GetForReviewBody(ISpecification<RegulatoryBodyAuditTrial> specification);

    Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<RegulatoryBodyAuditTrial> specification);
}