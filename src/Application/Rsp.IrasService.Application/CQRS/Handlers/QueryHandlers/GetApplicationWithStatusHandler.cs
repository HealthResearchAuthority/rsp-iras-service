using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationWithStatusHandler(ILogger<GetApplicationWithStatusHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationWithStatusQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationWithStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformationHp(nameof(request.ApplicationId), "Getting application with ID");

        return await applicationsService.GetApplication(request.ApplicationId, request.ApplicationStatus);
    }
}