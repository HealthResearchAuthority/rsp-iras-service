using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosuresBySponsorOrganisationUserIdHandler(IProjectClosureService projectClosureService) : IRequestHandler<GetProjectClosuresBySponsorOrganisationUserIdQuery, ProjectClosuresSearchResponse>
{
    public async Task<ProjectClosuresSearchResponse> Handle(GetProjectClosuresBySponsorOrganisationUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = await projectClosureService.GetProjectClosuresBySponsorOrganisationUserId
        (
            request.SponsorOrganisationUserId,
            request.SearchQuery,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection,
            request.RtsId
        );

        return response;
    }
}