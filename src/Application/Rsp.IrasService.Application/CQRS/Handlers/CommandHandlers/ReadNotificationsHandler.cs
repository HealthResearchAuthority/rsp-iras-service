using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class ReadNotificationsHandler(IUserNotificationsService userNotificationsService) :
    IRequestHandler<ReadNotificationsCommand>
{
    public async Task Handle(ReadNotificationsCommand request, CancellationToken cancellationToken)
    {
        await userNotificationsService.ReadNotifications(request.UserId);
    }
}