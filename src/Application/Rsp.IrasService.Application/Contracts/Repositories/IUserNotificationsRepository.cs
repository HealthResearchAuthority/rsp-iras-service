namespace Rsp.Service.Application.Contracts.Repositories;

public interface IUserNotificationsRepository
{
    Task AutoClearReadNotifications();
}