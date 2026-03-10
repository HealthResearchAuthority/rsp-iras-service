using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserNotificationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a user notification
    /// </summary>
    [HttpPost("create")]
    public async Task<UserNotificationResponse> CreateUserNotification(CreateUserNotificationRequest request)
    {
        return await mediator.Send(new CreateUserNotificationCommand(request));
    }

    /// <summary>
    /// Returns all notifications for a user, ordered by most recent first
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<UserNotificationsResponse> GetUserNotifications(
         string userId,
         int pageNumber,
         int pageSize,
         string sortField,
         string sortDirection,
         string? type = null)
    {
        return await mediator.Send(new GetUserNotificationsQuery(userId, pageNumber, pageSize, sortField, sortDirection, type));
    }

    /// <summary>
    /// Returns the count of unread notifications for a user
    /// </summary>
    [HttpGet("notifications/{userId}")]
    public async Task<int> GetUnreadUserNotificationsCount(string userId)
    {
        return await mediator.Send(new GetUnreadUserNotificationsCountQuery(userId));
    }

    /// <summary>
    /// Marks a user notification as read by setting the DateTimeSeen property to the current date and time
    /// </summary>
    [HttpPatch("read/{userId}")]
    public async Task<IActionResult> ReadNotifications(string userId)
    {
        await mediator.Send(new ReadNotificationsCommand(userId));
        return Ok();
    }

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