using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

public class AllReviewBodiesResponse
{
    public IEnumerable<ReviewBodyDto> ReviewBodies { get; set; } = [];

    public int TotalCount { get; set; }
}