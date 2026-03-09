using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

public class CreateUserNotificationCommand(CreateUserNotificationRequest request) : IRequest<UserNotificationResponse>
{
    public CreateUserNotificationRequest CreateUserNotificationRequest { get; set; } = request;
}