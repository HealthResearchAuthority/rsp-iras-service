using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.Contracts.Services;

public interface IReviewBodyAuditTrailService
{
    Task<ReviewBodyAuditTrailResponse> GetAuditTrailForReviewBody(Guid id, int skip, int take);
}