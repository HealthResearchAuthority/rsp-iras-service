using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to delete a project by its record ID.
/// </summary>
public class DeleteProjectCommand(string projectRecordId) : IRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the project record to delete.
    /// </summary>
    public string ProjectRecordId { get; set; } = projectRecordId;
}