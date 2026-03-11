using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IUserNotificationsRepository
{
    Task<UserNotification> CreateUserNotification(UserNotification userNotification);

    Task<(IEnumerable<UserNotification>, int)> GetUserNotifications
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