using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingHandler(IProjectClosureService projectClosureService) : IRequestHandler<GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery, ProjectClosuresSearchResponse>
{
    public async Task<ProjectClosuresSearchResponse> Handle(GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery request, CancellationToken cancellationToken)
    {
        var response = await projectClosureService.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging
        (
            request.SponsorOrganisationUserId,
            request.SearchQuery
        );

        return response;
    }
}