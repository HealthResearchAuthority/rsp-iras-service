using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.DTOS.Responses;

public class ModificationResponse
{
    public IEnumerable<ModificationDto> Modifications { get; set; } = [];
    public int TotalCount { get; set; }
}