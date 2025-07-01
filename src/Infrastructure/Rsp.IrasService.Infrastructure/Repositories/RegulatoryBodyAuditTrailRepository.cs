using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

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