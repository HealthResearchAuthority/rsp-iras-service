using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationsHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationsQuery, IEnumerable<ApplicationResponse>>
{
    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetApplications();
    }
}