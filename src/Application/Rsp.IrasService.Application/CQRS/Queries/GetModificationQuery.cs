using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationQuery(string projectModificationId) : BaseQuery, IRequest<ModificationResponse?>
{
    public string ProjectModificationId { get; } = projectModificationId;
}