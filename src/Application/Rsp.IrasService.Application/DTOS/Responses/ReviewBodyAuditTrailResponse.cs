using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

public class ReviewBodyAuditTrailResponse
{
    public IEnumerable<ReviewBodyAuditTrailDto> Items { get; set; } = Enumerable.Empty<ReviewBodyAuditTrailDto>();
    public int TotalCount { get; set; }
}