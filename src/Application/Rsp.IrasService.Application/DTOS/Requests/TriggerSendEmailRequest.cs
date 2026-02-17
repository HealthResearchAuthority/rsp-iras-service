namespace Rsp.Service.Application.DTOS.Requests;

public class TriggerSendEmailRequest
{
    public string EventType { get; set; } = null!;
    public IEnumerable<string> EmailRecipients { get; set; } = Enumerable.Empty<string>();
    public IDictionary<string, dynamic> Data { get; set; } = new Dictionary<string, dynamic>();
}