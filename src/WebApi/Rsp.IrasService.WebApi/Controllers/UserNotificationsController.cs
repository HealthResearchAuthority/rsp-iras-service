using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rsp.Service.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserNotificationsController(IMediator mediator) : ControllerBase
{
}