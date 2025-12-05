using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Extensions;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsBySponsorOrganisationUserIdHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsBySponsorOrganisationUserIdQuery, ModificationSearchResponse>
{
    public async Task<ModificationSearchResponse> Handle(GetModificationsBySponsorOrganisationUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = await projectModificationService.GetModificationsBySponsorOrganisationUserId
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