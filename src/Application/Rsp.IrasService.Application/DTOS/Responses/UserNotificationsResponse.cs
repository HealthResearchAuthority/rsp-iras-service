using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.DTOS.Responses;

public class UserNotificationsResponse
{
    public IEnumerable<UserNotificationResponse> Notifications { get; set; } = [];
    public int TotalCount { get; set; }
}