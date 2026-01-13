using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.Contracts.Services;

public interface IReviewBodyAuditTrailService
{
    Task<ReviewBodyAuditTrailResponse> GetAuditTrailForReviewBody(Guid id, int skip, int take);
}