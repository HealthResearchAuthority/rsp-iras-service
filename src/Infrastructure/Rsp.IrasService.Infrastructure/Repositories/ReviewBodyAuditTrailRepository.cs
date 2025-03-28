using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ReviewBodyAuditTrailRepository(IrasContext irasContext) : IReviewBodyAuditTrailRepository
{
    public IEnumerable<ReviewBodyAuditTrail> GetForReviewBody(ISpecification<ReviewBodyAuditTrail> specification)
    {
        return irasContext
            .ReviewBodiesAuditTrail
            .WithSpecification(specification);
    }

    public async Task<IEnumerable<ReviewBodyAuditTrail>> CreateAuditRecords(IEnumerable<ReviewBodyAuditTrail> reviewBodyAuditTrail)
    {
        await irasContext.ReviewBodiesAuditTrail.AddRangeAsync(reviewBodyAuditTrail);
        await irasContext.SaveChangesAsync();

        return reviewBodyAuditTrail;
    }

    public async Task<int> GetTotalNumberOfRecordsForReviewBody(ISpecification<ReviewBodyAuditTrail> specification)
    {
        return await irasContext
            .ReviewBodiesAuditTrail
            .WithSpecification(specification)
            .CountAsync();
    }
}