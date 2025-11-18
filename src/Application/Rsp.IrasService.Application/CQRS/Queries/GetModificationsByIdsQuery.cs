using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationsByIdsQuery(List<string> Ids) : IRequest<ModificationSearchResponse>
{
    public List<string> Ids { get; } = Ids;
}