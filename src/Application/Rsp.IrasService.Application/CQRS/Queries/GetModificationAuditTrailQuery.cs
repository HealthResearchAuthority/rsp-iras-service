using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationAuditTrailQuery(Guid modificationId) : IRequest<ModificationAuditTrailResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project modification.
    /// </summary>
    public Guid ProjectModificationId { get; } = modificationId;
}