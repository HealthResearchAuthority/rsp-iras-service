using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

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