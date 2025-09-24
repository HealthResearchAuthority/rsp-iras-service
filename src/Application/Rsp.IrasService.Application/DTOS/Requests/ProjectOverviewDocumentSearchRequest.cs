namespace Rsp.IrasService.Application.DTOS.Requests;

public class ProjectOverviewDocumentSearchRequest
{
    public string? IrasId { get; set; }
    public Dictionary<string, string> DocumentTypes { get; set; } = [];
}