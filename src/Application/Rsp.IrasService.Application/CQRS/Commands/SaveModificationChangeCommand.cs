using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

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