using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;

namespace Rsp.Service.Services;

public class UserNotificationsService(IUserNotificationsRepository userNotificationsRepository) : IUserNotificationsService
{
    public async Task AutoClearReadNotifications(int daysUntilAutoCleared)
    {
        await userNotificationsRepository.AutoClearReadNotifications(daysUntilAutoCleared);
    }
}