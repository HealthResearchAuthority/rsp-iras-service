using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.DTOS.Responses;

public class AllReviewBodiesResponse
{
    public IEnumerable<ReviewBodyDto> ReviewBodies { get; set; } = [];

    public int TotalCount { get; set; }
}