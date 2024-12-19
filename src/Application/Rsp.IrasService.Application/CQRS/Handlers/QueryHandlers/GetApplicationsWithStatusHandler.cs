using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsWithStatusHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationsWithStatusQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsWithStatusQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetApplications(request.ApplicationStatus);
    }
}