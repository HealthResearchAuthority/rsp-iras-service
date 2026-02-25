using Rsp.Service.Application.Contracts.Repositories;

namespace Rsp.Service.Infrastructure.Repositories;

public class UserNotificationsRepository(IrasContext irasContext) : IUserNotificationsRepository
{
}