using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class EventType : ICreatable
{
    public string Id { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
}