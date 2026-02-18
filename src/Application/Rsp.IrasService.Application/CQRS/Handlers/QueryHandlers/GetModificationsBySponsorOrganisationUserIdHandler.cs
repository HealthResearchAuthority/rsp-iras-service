using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

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
            request.SortDirection,
            request.RtsId
        );

        return response;
    }
}