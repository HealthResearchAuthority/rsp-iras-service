namespace Rsp.Service.Domain.Entities;

public class EventType
{
    public string Id { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
}