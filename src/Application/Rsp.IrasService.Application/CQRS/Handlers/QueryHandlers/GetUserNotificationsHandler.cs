using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetUserNotificationsHandler(IUserNotificationsService userNotificationsService) :
    IRequestHandler<GetUserNotificationsQuery, IEnumerable<UserNotificationResponse>>
{
    public async Task<IEnumerable<UserNotificationResponse>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await userNotificationsService.GetUserNotifications(request.UserId);
    }
}