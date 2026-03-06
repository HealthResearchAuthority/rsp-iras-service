using Rsp.Logging.Interceptors;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.Contracts.Services;

public interface IUserNotificationsService : IInterceptable
{
    Task AutoClearReadNotifications();
}