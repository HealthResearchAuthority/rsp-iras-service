using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithRespondentHandler(ILogger<GetApplicationsWithRespondentHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithRespondentQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsWithRespondentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all applications for respondent: {RespondentId}", request.RespondentId);

        return await applicationsService.GetRespondentApplications(request.RespondentId);
    }
}