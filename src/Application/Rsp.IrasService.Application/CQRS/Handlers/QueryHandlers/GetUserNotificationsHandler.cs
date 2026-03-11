using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetUserNotificationsHandler(IUserNotificationsService userNotificationsService) :
    IRequestHandler<GetUserNotificationsQuery, UserNotificationsResponse>
{
    public async Task<UserNotificationsResponse> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await userNotificationsService.GetUserNotifications
        (
            request.UserId,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection,
            request.Type
        );
    }
}