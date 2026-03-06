using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserNotificationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Automatically clears read user notifications
    /// </summary>
    [HttpGet("autoclear-read-notifications")]
    public async Task<IActionResult> AutoClearReadNotifications(int daysUntilAutoCleared)
    {
        await mediator.Send(new GetAutoClearUserNotificationsCommand()
        {
            DaysUntilAutoCleared = daysUntilAutoCleared
        });
        return Ok();
    }
}
