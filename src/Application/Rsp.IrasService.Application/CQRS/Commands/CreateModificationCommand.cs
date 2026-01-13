using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

/// <summary>
/// Command to create a new modification using the provided modification request.
/// </summary>
public class CreateModificationCommand(ModificationRequest modificationRequest) : IRequest<ModificationResponse>
{
    /// <summary>
    /// Gets or sets the modification request containing the details for the modification to be created.
    /// </summary>
    public ModificationRequest ModificationRequest { get; set; } = modificationRequest;
}