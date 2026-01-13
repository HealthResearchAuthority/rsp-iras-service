using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

/// <summary>
/// Represents a response model for a modification document associated with a project change.
/// </summary>
public class ModificationDocumentResponse
{
    /// <summary>
    /// A collection of modification DTOs associated with this response.
    /// </summary>
    public IEnumerable<ProjectOverviewDocumentDto> Documents { get; set; } = [];
}