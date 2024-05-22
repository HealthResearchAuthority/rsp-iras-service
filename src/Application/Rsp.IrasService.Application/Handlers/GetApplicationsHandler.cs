using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Queries;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Handlers;

public class GetApplicationsHandler(ILogger<GetApplicationsHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationsQuery, IEnumerable<GetApplicationResponse>>
{
    public async Task<IEnumerable<GetApplicationResponse>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all applications");

        return await applicationsService.GetApplications();
    }
}