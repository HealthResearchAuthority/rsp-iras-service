using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Interceptors;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.Contracts.Services;

public interface IUserNotificationsService : IInterceptable
{
    Task<UserNotificationResponse> CreateUserNotification(CreateUserNotificationRequest createUserNotificationRequest);

    Task<UserNotificationsResponse> GetUserNotifications
    (
        string userId,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? type = null
    );

    Task<int> GetUnreadUserNotificationsCount(string userId);

    Task ReadNotifications(string userId);

    Task AutoClearReadNotifications(int daysUntilAutoCleared);
}