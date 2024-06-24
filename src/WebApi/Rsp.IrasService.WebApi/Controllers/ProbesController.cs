using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.Authorization.Attributes;
using Rsp.IrasService.Application.Commands;
using Rsp.IrasService.Application.Queries;
using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProbesController: ControllerBase
{
    [HttpGet("liveness")]
    public IActionResult Liveness()
    {
        return Ok();
    }

    [HttpGet("readiness")]
    public IActionResult Readiness()
    {
        return Ok();
    }

    [HttpGet("startup")]
    public IActionResult Startup()
    {
        return Ok();
    }

}