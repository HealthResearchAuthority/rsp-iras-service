using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetUserNotificationsQuery(string userId) : IRequest<IEnumerable<UserNotificationResponse>>
{
    public string UserId { get; set; } = userId;
}