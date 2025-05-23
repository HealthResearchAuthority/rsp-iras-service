using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;

namespace Rsp.IrasService.Services;

public class ReviewBodyAuditTrailService(IReviewBodyAuditTrailRepository repo) : IReviewBodyAuditTrailService
{
    public async Task<ReviewBodyAuditTrailResponse> GetAuditTrailForReviewBody(Guid id, int skip, int take)
    {
        var result = new ReviewBodyAuditTrailResponse();

        var itemsSpecification = new GetReviewBodyAuditTrailSpecification(id, skip, take);
        var countSpecification = new GetReviewBodyAuditTrailCountSpecification(id);

        var items = repo.GetForReviewBody(itemsSpecification);
        var totalCount = await repo.GetTotalNumberOfRecordsForReviewBody(countSpecification);

        var dtos = items.Adapt<IEnumerable<ReviewBodyAuditTrailDto>>();

        result.Items = dtos;
        result.TotalCount = totalCount;

        return result;
    }
}