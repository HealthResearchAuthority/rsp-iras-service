using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationWithStatusHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationWithStatusQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationWithStatusQuery request, CancellationToken cancellationToken)
    {
        var application = await applicationsService.GetApplication(request.ApplicationId, request.ApplicationStatus);

        return application.FilterSingleOrNull(request, a => a.Status)!;
    }
}