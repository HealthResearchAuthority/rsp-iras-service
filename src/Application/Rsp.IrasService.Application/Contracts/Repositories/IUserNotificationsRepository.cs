using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface IUserNotificationsRepository
{
    Task<UserNotification> CreateUserNotification(UserNotification userNotification);

    Task<IEnumerable<UserNotification>> GetUserNotifications(string userId);

    Task<int> GetUnreadUserNotificationsCount(string userId);

    Task ReadNotifications(string userId);

    Task AutoClearReadNotifications(int daysUntilAutoCleared);
}