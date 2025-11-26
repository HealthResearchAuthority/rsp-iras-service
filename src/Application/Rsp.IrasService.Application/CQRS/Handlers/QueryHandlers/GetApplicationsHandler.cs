using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationsQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = await applicationsService.GetApplications();

        return applications.FilterByAllowedStatuses(request, a => a.Status);
    }
}