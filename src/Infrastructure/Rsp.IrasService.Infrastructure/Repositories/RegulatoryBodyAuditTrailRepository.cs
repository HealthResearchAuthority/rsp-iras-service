using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

public class RegulatoryBodyAuditTrailRepository(IrasContext irasContext) : IRegulatoryBodyAuditTrailRepository
{
    public IEnumerable<RegulatoryBodyAuditTrail> GetForReviewBody(ISpecification<RegulatoryBodyAuditTrail> specification)
    {
        return irasContext
            .RegulatoryBodiesAuditTrail
            .WithSpecification(specification);
    }

    public async Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<RegulatoryBodyAuditTrail> specification)
    {
        return await irasContext
            .RegulatoryBodiesAuditTrail
            .WithSpecification(specification)
            .CountAsync();
    }
}