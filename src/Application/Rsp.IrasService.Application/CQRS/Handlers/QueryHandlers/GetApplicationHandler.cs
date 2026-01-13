using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        var application = await applicationsService.GetApplication(request.ApplicationId);
        return application.FilterSingleOrNull(request, a => a.Status)!;
    }
}