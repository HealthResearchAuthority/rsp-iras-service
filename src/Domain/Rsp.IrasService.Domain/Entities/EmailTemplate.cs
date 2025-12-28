using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class EmailTemplate : ICreatable, IUpdatable
{
    public int Id { get; set; }
    public string TemplateId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string EventTypeId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }

    // nabigation properties
    public EventType EventType { get; set; } = null!;
}