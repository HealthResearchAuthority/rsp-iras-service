using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IRegulatoryBodyAuditTrailRepository
{
    IEnumerable<RegulatoryBodyAuditTrail> GetForReviewBody(ISpecification<RegulatoryBodyAuditTrail> specification);

    Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<RegulatoryBodyAuditTrail> specification);
}