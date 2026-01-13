namespace Rsp.Service.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a project modification.
/// </summary>
public record UpdateModificationChangeRequest : ModificationChangeRequest
{
    /// <summary>
    /// Ranking type of the modification change
    /// </summary>
    public string? ModificationType { get; set; }

    /// <summary>
    /// Ranking category of the modification change
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Ranking review type of the modification change
    /// </summary>
    public string? ReviewType { get; set; }
}