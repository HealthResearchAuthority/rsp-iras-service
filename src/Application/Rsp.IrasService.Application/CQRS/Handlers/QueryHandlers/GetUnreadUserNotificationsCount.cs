using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetUnreadUserNotificationsCount(IUserNotificationsService userNotificationsService) :
    IRequestHandler<GetUnreadUserNotificationsCountQuery, int>
{
    public async Task<int> Handle(GetUnreadUserNotificationsCountQuery request, CancellationToken cancellationToken)
    {
        return await userNotificationsService.GetUnreadUserNotificationsCount(request.UserId);
    }
}