using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationQuery : BaseQuery, IRequest<ModificationResponse?>
{
    public required string ProjectRecordId { get; set; }
    public required Guid ProjectModificationId { get; set; }
}