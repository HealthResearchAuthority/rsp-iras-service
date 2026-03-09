using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Rsp.Service.Domain.Interfaces;
using Rsp.Service.Infrastructure.Helpers;

namespace Rsp.Service.Infrastructure.Interceptors;

[ExcludeFromCodeCoverage]
public class UserNotificationsInterceptor(UserNotificationsHandler userNotificationsHandler) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
    (
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is not IrasContext db)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = db.ChangeTracker.Entries()
            .Where(e => e.Entity is INotifiable)
            .ToList();

        foreach (var entry in entries)
        {
            db.UserNotifications.AddRange(userNotificationsHandler.CreateUserNotifications(entry));
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}