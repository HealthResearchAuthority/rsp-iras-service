namespace Rsp.IrasService.Domain.Entities
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string TemplateId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int EventTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;

        // nabigation properties
        public EventType EventType { get; set; }
    }
}
