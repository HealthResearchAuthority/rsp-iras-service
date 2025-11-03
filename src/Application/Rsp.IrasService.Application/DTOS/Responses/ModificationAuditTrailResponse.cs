namespace Rsp.IrasService.Application.DTOS.Responses;

public class ModificationAuditTrailResponse
{
    public IEnumerable<ModificationAuditTrailDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}