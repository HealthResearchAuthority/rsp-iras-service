using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationDocumentsByTypeQuery : BaseQuery, IRequest<IEnumerable<ModificationDocumentDto>>
{
    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; }

    /// <summary>
    /// Gets or sets the type identifier.
    /// </summary>
    public string? DocumentTypeId { get; set; } = null!;
}