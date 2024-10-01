using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationHandler(ILogger<GetApplicationHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting application with ID = {Id}", request.ApplicationId);

        return await applicationsService.GetApplication(request.ApplicationId);
    }
}