using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Commands;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Handlers;

public class CreateApplicationHandler(ILogger<CreateApplicationHandler> logger, IApplicationsService applicationsService) : IRequestHandler<CreateApplicationCommand, CreateApplicationResponse>
{
    public async Task<CreateApplicationResponse> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating IRAS application");

        return await applicationsService.CreateApplication(request.CreateApplicationRequest);
    }
}