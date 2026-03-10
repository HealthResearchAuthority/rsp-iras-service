using Rsp.Logging.Interceptors;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.Contracts.Services;

public interface IUserNotificationsService : IInterceptable
{
    Task<UserNotificationResponse> CreateUserNotification(CreateUserNotificationRequest createUserNotificationRequest);

    Task<IEnumerable<UserNotificationResponse>> GetUserNotifications(string userId);

    Task<int> GetUnreadUserNotificationsCount(string userId);

    Task ReadNotifications(string userId);

    Task AutoClearReadNotifications(int daysUntilAutoCleared);
}