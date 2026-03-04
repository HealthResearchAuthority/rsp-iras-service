using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

public class ProjectDocumentsAuditTrailResponse
{
    public IEnumerable<ModificationDocumentsAuditTrailDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}