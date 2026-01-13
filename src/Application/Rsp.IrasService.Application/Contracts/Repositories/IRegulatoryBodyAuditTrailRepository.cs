using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IRegulatoryBodyAuditTrailRepository
{
    IEnumerable<RegulatoryBodyAuditTrail> GetForReviewBody(ISpecification<RegulatoryBodyAuditTrail> specification);

    Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<RegulatoryBodyAuditTrail> specification);
}