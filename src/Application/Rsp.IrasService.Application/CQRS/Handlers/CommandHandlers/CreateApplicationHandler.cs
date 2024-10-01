using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class CreateApplicationHandler(ILogger<CreateApplicationHandler> logger, IApplicationsService applicationsService) : IRequestHandler<CreateApplicationCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating IRAS application");

        return await applicationsService.CreateApplication(request.CreateApplicationRequest);
    }
}