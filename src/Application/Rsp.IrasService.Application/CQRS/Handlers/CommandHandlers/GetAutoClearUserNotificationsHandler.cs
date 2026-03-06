using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetAutoClearUserNotificationsHandler(IUserNotificationsService userNotificationsService)
    : IRequestHandler<GetAutoClearUserNotificationsCommand>
{
    public async Task Handle(GetAutoClearUserNotificationsCommand request, CancellationToken cancellationToken)
    {
        await userNotificationsService.AutoClearReadNotifications();
    }
}