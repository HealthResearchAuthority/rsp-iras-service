using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;

namespace Rsp.Service.Infrastructure.Repositories;

public class UserNotificationsRepository(IrasContext irasContext) : IUserNotificationsRepository
{
    public async Task AutoClearReadNotifications(int daysUntilAutoCleared)
    {
        var cutoff = DateTime.UtcNow.AddDays(-daysUntilAutoCleared);

        await irasContext.UserNotifications
            .Where(n => n.DateTimeSeen.HasValue && n.DateTimeSeen.Value < cutoff)
            .ExecuteDeleteAsync();
    }
}