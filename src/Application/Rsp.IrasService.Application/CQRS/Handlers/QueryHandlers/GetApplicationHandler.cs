using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        var application = await applicationsService.GetApplication(request.ApplicationId);
        return application.FilterSingleOrNull(request, a => a.Status)!;
    }
}