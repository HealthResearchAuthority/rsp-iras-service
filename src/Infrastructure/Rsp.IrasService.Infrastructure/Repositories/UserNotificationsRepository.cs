using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

public class UserNotificationsRepository(IrasContext irasContext) : IUserNotificationsRepository
{
    public async Task<UserNotification> CreateUserNotification(UserNotification userNotification)
    {
        var entity = irasContext.UserNotifications.Add(userNotification);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<int> GetUnreadUserNotificationsCount(string userId)
    {
        return await irasContext.UserNotifications
            .Where(n => n.UserId == Guid.Parse(userId) && !n.DateTimeSeen.HasValue)
            .CountAsync();
    }

    public async Task<(IEnumerable<UserNotification>, int)> GetUserNotifications
    (
        string userId,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string? type = null
    )
    {
        // get all notifications for user
        var notifications = irasContext.UserNotifications
            .Where(n => n.UserId == Guid.Parse(userId));

        // filter by type if provided
        if (!string.IsNullOrEmpty(type))
        {
            notifications = notifications.Where(n => n.Type == type);
        }

        // execute sorting
        notifications = (sortField, sortDirection.ToLower()) switch
        {
            (nameof(UserNotification.Text), "asc") => notifications.OrderBy(x => x.Text),
            (nameof(UserNotification.Text), "desc") => notifications.OrderByDescending(x => x.Text),
            (nameof(UserNotification.Type), "asc") => notifications.OrderBy(x => x.Type),
            (nameof(UserNotification.Type), "desc") => notifications.OrderByDescending(x => x.Type),
            (nameof(UserNotification.DateTimeCreated), "asc") => notifications.OrderBy(x => x.DateTimeCreated),
            (nameof(UserNotification.DateTimeCreated), "desc") => notifications.OrderByDescending(x => x.DateTimeCreated),
            ("Days", "asc") => notifications.OrderByDescending(x => x.DateTimeCreated),
            ("Days", "desc") => notifications.OrderBy(x => x.DateTimeCreated),

            _ => notifications.OrderByDescending(x => x.DateTimeCreated)
        };

        var totalCount = await notifications.CountAsync();

        // execute pagination and return results
        var result = await notifications
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (result, totalCount);
    }

    public async Task ReadNotifications(string userId)
    {
        var notifications = await irasContext.UserNotifications
            .Where(n => n.UserId == Guid.Parse(userId) && !n.DateTimeSeen.HasValue)
            .ToListAsync();

        foreach (var notification in notifications)
        {
            notification.DateTimeSeen = DateTime.UtcNow;
        }

        await irasContext.SaveChangesAsync();
    }

    public async Task AutoClearReadNotifications(int daysUntilAutoCleared)
    {
        var cutoff = DateTime.UtcNow.AddDays(-daysUntilAutoCleared);

        await irasContext.UserNotifications
            .Where(n => n.DateTimeSeen.HasValue && n.DateTimeSeen.Value < cutoff)
            .ExecuteDeleteAsync();
    }
}