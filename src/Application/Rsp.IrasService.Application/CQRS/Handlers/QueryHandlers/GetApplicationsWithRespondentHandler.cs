using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithRespondentHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithRespondentQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsWithRespondentQuery request, CancellationToken cancellationToken)
    {
        var applications = await applicationsService.GetRespondentApplications(request.RespondentId);

        return applications.FilterByAllowedStatuses(request, a => a.Status);
    }
}