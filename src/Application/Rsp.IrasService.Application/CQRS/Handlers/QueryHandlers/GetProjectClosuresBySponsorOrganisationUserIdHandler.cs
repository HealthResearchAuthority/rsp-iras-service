using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

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
            request.SortDirection
        );

        return response;
    }
}