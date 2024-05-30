using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Queries;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.QueryHandlers;

public class GetApplicationsWithStatusHandler(ILogger<GetApplicationsWithStatusHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithStatusQuery, IEnumerable<GetApplicationResponse>>
{
    public async Task<IEnumerable<GetApplicationResponse>> Handle(GetApplicationsWithStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all applications");

        return await applicationsService.GetApplications(request.ApplicationStatus);
    }
}