using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

/// <summary>
/// Command to save modification answers for a project.
/// </summary>
public class SaveModificationChangeAnswersCommand(ModificationChangeAnswersRequest request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification answers request containing the answers to be saved.
    /// </summary>
    public ModificationChangeAnswersRequest ModificationAnswersRequest { get; set; } = request;
}