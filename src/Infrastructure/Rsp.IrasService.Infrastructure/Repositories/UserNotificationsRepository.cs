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

    public async Task<IEnumerable<UserNotification>> GetUserNotifications(string userId)
    {
        return await irasContext.UserNotifications
            .Where(n => n.UserId == Guid.Parse(userId))
            .OrderByDescending(n => n.DateTimeCreated)
            .ToListAsync();
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