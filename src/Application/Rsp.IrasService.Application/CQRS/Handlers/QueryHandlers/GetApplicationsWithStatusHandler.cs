using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithStatusHandler(ILogger<GetApplicationsWithStatusHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithStatusQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsWithStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all applications");

        return await applicationsService.GetApplications(request.ApplicationStatus);
    }
}