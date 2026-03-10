namespace Rsp.Service.Application.DTOS.Requests;

public class CreateUserNotificationRequest
{
    public string UserId { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? TargetUrl { get; set; }
    public string Type { get; set; } = null!;
    public string? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
}