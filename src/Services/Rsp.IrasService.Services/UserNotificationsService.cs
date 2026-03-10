using Mapster;
using Rsp.IrasService.Application.DTOS.Responses;
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

    public async Task<UserNotificationsResponse> GetUserNotifications
    (
        string userId,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? type = null
    )
    {
        var response = new UserNotificationsResponse();
        var userNotificationsResponse = await userNotificationsRepository.GetUserNotifications
        (
            userId,
            pageNumber,
            pageSize,
            sortField,
            sortDirection,
            type
        );

        response.Notifications = userNotificationsResponse.Item1.Adapt<IEnumerable<UserNotificationResponse>>();
        response.TotalCount = userNotificationsResponse.Item2;

        return response;
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