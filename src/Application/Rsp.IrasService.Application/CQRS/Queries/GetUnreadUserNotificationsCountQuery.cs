using MediatR;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetUnreadUserNotificationsCountQuery(string userId) : IRequest<int>
{
    public string UserId { get; set; } = userId;
}