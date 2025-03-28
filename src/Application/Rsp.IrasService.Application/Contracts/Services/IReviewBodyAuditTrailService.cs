using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.Contracts.Services;

public interface IReviewBodyAuditTrailService
{
    Task<ReviewBodyAuditTrailResponse> GetAuditTrailForReviewBody(Guid id, int skip, int take);

    public Task<IEnumerable<ReviewBodyAuditTrailDto>> LogRecords(IEnumerable<ReviewBodyAuditTrailDto> records);

    public IEnumerable<ReviewBodyAuditTrailDto> GenerateAuditTrailDtoFromReviewBody(ReviewBodyDto dto, string userId, string action, ReviewBodyDto? oldDto = null);
}