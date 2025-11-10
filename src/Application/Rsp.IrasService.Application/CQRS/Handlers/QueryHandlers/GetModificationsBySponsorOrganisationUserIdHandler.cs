using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsBySponsorOrganisationUserIdHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsBySponsorOrganisationUserIdQuery, ModificationResponse>
{
    public async Task<ModificationResponse> Handle(GetModificationsBySponsorOrganisationUserIdQuery request, CancellationToken cancellationToken)
    {
        return await projectModificationService.GetModificationsBySponsorOrganisationUserId
        (
            request.SponsorOrganisationUserId,
            request.SearchQuery,
            request.PageNumber,
            request.PageSize,
            request.SortField,
            request.SortDirection
        );
    }
}