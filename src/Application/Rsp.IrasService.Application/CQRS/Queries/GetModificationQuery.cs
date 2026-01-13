using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationQuery : BaseQuery, IRequest<ModificationResponse?>
{
    public required string ProjectRecordId { get; set; }
    public required Guid ProjectModificationId { get; set; }
}