namespace Rsp.IrasService.Domain.Entities
{
    public class EventType
    {
        public int Id { get; set; }
        public string NotificationType { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
