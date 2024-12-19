using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetApplication(request.ApplicationId);
    }
}