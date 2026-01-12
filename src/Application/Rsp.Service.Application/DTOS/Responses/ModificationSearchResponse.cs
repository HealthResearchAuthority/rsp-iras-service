using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.DTOS.Responses;

/// <summary>
/// Represents the response containing details about a modification, including metadata and a list of modifications.
/// </summary>
public record ModificationSearchResponse : ModificationResponse
{
    /// <summary>
    /// A collection of modification DTOs associated with this response.
    /// </summary>
    public IEnumerable<ModificationDto> Modifications { get; set; } = [];

    /// <summary>
    /// The total count of modifications associated with the application.
    /// </summary>
    public int TotalCount { get; set; }
}