using System.Text.Json.Serialization;

namespace Rsp.IrasService.Application.DTOS.Responses;

public class UpdateDocumentScanStatusResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
    public string CorellationId { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public string Message { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public ErrorResponse ErrorResponse { get; set; } = new();
}