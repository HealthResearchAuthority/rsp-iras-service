namespace Rsp.IrasService.Domain.Entities;

public class ModificationSpecificAreaOfChange
{
    /// <summary>
    /// Gets or sets the unique identifier for this modification specific area of change.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name for this modification specific area of change.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the journey type for this modification specific area of change.
    /// </summary>
    public string? JourneyType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for this modification specific area of change.
    /// </summary>
    public int ModificationAreaOfChangeId { get; set; }
}