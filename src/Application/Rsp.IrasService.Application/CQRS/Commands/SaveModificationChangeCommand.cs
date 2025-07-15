using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to save a modification change for a project.
/// </summary>
public class SaveModificationChangeCommand(ModificationChangeRequest request) : IRequest<ModificationChangeResponse>
{
    /// <summary>
    /// Gets or sets the modification change request containing the change details.
    /// </summary>
    public ModificationChangeRequest ModificationChangeRequest { get; set; } = request;
}