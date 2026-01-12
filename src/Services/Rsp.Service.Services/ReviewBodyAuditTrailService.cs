using Mapster;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;

namespace Rsp.Service.Services;

public class ReviewBodyAuditTrailService(IRegulatoryBodyAuditTrailRepository repo) : IReviewBodyAuditTrailService
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