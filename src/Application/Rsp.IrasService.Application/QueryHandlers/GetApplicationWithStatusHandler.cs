using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Queries;
using Rsp.IrasService.Application.Responses;
using Rsp.Logging.Extensions;

namespace Rsp.IrasService.Application.QueryHandlers;

public class GetApplicationWithStatusHandler(ILogger<GetApplicationWithStatusHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationWithStatusQuery, GetApplicationResponse>
{
    public async Task<GetApplicationResponse> Handle(GetApplicationWithStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformationHp(nameof(request.Id), "Getting application with ID");

        return await applicationsService.GetApplication(request.Id, request.ApplicationStatus);
    }
}