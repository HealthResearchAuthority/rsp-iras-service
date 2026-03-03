using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

/// <summary>
/// Command to duplciate modification using the provided modification request.
/// </summary>
public class DuplicateModificationCommand(DuplicateModificationRequest duplicateModificationRequest) : IRequest<ModificationResponse>
{
    /// <summary>
    /// Gets or sets the modification request containing the details for the modification to be duplicated.
    /// </summary>
    public DuplicateModificationRequest ModificationRequest { get; set; } = duplicateModificationRequest;
}