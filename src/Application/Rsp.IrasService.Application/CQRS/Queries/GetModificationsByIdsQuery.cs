using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationsByIdsQuery(List<string> Ids) : BaseQuery, IRequest<ModificationSearchResponse>
{
    public List<string> Ids { get; } = Ids;
}