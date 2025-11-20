namespace Rsp.IrasService.Application.DTOS.Responses;

public class ProjectRecordAuditTrailResponse
{
    public IEnumerable<ProjectRecordAuditTrailDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}