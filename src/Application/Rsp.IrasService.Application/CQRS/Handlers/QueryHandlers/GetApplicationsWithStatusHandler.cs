using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithStatusHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithStatusQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsWithStatusQuery request, CancellationToken cancellationToken)
    {
        var applications = await applicationsService.GetApplications(request.ApplicationStatus);

        return applications.FilterByAllowedStatuses(request, a => a.Status);
    }
}