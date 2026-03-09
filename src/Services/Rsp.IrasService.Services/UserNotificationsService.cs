using Mapster;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

public class UserNotificationsService(IUserNotificationsRepository userNotificationsRepository) : IUserNotificationsService
{
    public async Task<UserNotificationResponse> CreateUserNotification(CreateUserNotificationRequest createUserNotificationRequest)
    {
        var userNotification = createUserNotificationRequest.Adapt<UserNotification>();

        var createdNotification = await userNotificationsRepository.CreateUserNotification(userNotification);

        return createdNotification.Adapt<UserNotificationResponse>();
    }

    public Task<int> GetUnreadUserNotificationsCount(string userId)
    {
        return userNotificationsRepository.GetUnreadUserNotificationsCount(userId);
    }

    public async Task<IEnumerable<UserNotificationResponse>> GetUserNotifications(string userId)
    {
        var userNotifications = await userNotificationsRepository.GetUserNotifications(userId);

        return userNotifications.Adapt<IEnumerable<UserNotificationResponse>>();
    }

    public Task ReadNotifications(string userId)
    {
        return userNotificationsRepository.ReadNotifications(userId);
    }

    public async Task AutoClearReadNotifications(int daysUntilAutoCleared)
    {
        await userNotificationsRepository.AutoClearReadNotifications(daysUntilAutoCleared);
    }
}