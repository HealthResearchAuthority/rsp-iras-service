using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class ReadNotificationsCommand(string userId) : IRequest
{
    public string UserId { get; set; } = userId;
}