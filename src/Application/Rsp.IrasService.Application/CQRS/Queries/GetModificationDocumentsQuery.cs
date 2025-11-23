using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

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
    /// Gets or sets the project personnel identifier.
    /// </summary>
    public string? ProjectPersonnelId { get; set; }
}