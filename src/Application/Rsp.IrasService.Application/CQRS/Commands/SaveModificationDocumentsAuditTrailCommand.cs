using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

/// <summary>
/// Command to save documents audit trail for a modification.
/// </summary>
public class SaveModificationDocumentsAuditTrailCommand(List<ModificationDocumentsAuditTrailDto> request) : IRequest
{
    /// <summary>
    /// Gets or sets the documents audit trail request containing the description to be saved.
    /// </summary>
    public List<ModificationDocumentsAuditTrailDto> DocumentsAuditTrailRequest { get; set; } = request;
}