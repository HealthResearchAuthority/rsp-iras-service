using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class CreateUserNotificationHandler(IUserNotificationsService userNotificationsService) :
    IRequestHandler<CreateUserNotificationCommand, UserNotificationResponse>
{
    public async Task<UserNotificationResponse> Handle(CreateUserNotificationCommand request, CancellationToken cancellationToken)
    {
        return await userNotificationsService.CreateUserNotification(request.CreateUserNotificationRequest);
    }
}