using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ReviewBodyAuditTrailRepository(IrasContext irasContext) : IReviewBodyAuditTrailRepository
{
    public IEnumerable<RegulatoryBodyAuditTrial> GetForReviewBody(ISpecification<RegulatoryBodyAuditTrial> specification)
    {
        return irasContext
            .RegulatoryBodyAuditTrial
            .WithSpecification(specification);
    }

    public async Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<RegulatoryBodyAuditTrial> specification)
    {
        return await irasContext
            .RegulatoryBodyAuditTrial
            .WithSpecification(specification)
            .CountAsync();
    }
}