using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.DTOS.Responses;

/// <summary>
/// Represents the response containing details about a modification, including metadata and a list of modifications.
/// </summary>
public record ModificationResponse
{
    /// <summary>
    /// The unique identifier for the modification record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique record identifier for the project.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// The sequential modification number for the project record.
    /// </summary>
    public int ModificationNumber { get; set; }

    /// <summary>
    /// The unique string identifier for the modification.
    /// </summary>
    public string ModificationIdentifier { get; set; } = null!;

    /// <summary>
    /// The current status of the project modification.
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// The user ID of the person who created the modification.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// The user ID of the person who last updated the modification.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    /// <summary>
    /// The date and time when the modification was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// The date and time when the modification was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// A collection of modification DTOs associated with this response.
    /// </summary>
    public IEnumerable<ModificationDto> Modifications { get; set; } = [];

    /// <summary>
    /// The total count of modifications associated with the application.
    /// </summary>
    public int TotalCount { get; set; }
}