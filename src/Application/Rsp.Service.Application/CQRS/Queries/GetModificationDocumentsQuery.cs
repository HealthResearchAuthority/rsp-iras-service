using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationDocumentsQuery : BaseQuery, IRequest<IEnumerable<ModificationDocumentDto>>
{
    /// <summary>
    /// Gets or sets the project modification identifier.
    /// </summary>
    public required Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public required string ProjectRecordId { get; set; }

    /// <summary>
    /// Gets or sets the project user identifier.
    /// </summary>
    public string? UserId { get; set; }
}