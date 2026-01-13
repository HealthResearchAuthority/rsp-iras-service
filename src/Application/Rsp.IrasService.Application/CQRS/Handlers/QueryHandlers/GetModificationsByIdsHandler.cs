using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Extensions;

namespace Rsp.Service.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsByIdsHandler(IProjectModificationService projectModificationService) : IRequestHandler<GetModificationsByIdsQuery, ModificationSearchResponse>
{
    public async Task<ModificationSearchResponse> Handle(GetModificationsByIdsQuery request, CancellationToken cancellationToken)
    {
        var response = await projectModificationService.GetModificationsByIds(request.Ids);

        return response;
    }
}