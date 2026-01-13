using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationAuditTrailQuery : IRequest<ModificationAuditTrailResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project modification.
    /// </summary>
    public required Guid ProjectModificationId { get; set; }
}