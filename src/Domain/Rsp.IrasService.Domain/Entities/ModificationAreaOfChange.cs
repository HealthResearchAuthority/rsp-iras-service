namespace Rsp.IrasService.Domain.Entities;

public class ModificationAreaOfChange
{
    /// <summary>
    /// Gets or sets the unique identifier for this modification area of change.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name for this modification area of change.
    /// </summary>
    public string Name { get; set; }

    // navigation properties
    public ICollection<ModificationSpecificAreaOfChange> ModificationSpecificAreaOfChanges { get; set; } = [];
}