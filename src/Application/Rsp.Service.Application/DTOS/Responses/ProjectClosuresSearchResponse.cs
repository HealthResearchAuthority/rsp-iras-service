namespace Rsp.Service.Application.DTOS.Responses;

/// <summary>
/// Represents the response containing details about a project closures, including metadata and a list of project closures.
/// </summary>
public record ProjectClosuresSearchResponse
{
    /// <summary>
    /// A collection of project closures associated with this response.
    /// </summary>
    public IEnumerable<ProjectClosureResponse> ProjectClosures { get; set; } = [];

    /// <summary>
    /// The total count of modifications associated with the application.
    /// </summary>
    public int TotalCount { get; set; }
}