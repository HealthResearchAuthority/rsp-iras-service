using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class UpdateApplicationHandler(ILogger<UpdateApplicationHandler> logger, IApplicationsService applicationsService) : IRequestHandler<UpdateApplicationCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        var applicationId = request.UpdateApplicationRequest.ApplicationId;

        logger.LogInformation("Update IRAS application with Application Id: {ApplicationId}", applicationId);

        return await applicationsService.UpdateApplication(request.UpdateApplicationRequest);
    }
}