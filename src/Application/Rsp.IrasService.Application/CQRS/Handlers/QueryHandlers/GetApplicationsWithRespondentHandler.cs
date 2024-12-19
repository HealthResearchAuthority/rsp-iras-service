using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithRespondentHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithRespondentQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsWithRespondentQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetRespondentApplications(request.RespondentId);
    }
}