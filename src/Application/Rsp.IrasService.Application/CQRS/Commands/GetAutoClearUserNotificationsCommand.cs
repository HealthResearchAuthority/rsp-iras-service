using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class GetAutoClearUserNotificationsCommand : IRequest
{
    public int DaysUntilAutoCleared { get; set; }
}