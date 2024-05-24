using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Commands;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.CommandHandlers;

public class UpdateApplicationHandler(ILogger<UpdateApplicationHandler> logger, IApplicationsService applicationsService) : IRequestHandler<UpdateApplicationCommand, CreateApplicationResponse>
{
    public async Task<CreateApplicationResponse> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update IRAS application with ID: {id}", request.Id);

        return await applicationsService.UpdateApplication(request.Id, request.CreateApplicationRequest);
    }
}