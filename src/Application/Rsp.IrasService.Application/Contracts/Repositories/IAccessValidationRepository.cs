namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IAccessValidationRepository
{
    /// <summary>
    /// Checks if the user has access to the specified project record or modification.
    /// If modificationId is provided it will check modification-level access paths (sponsor/reviewer) first.
    /// If projectRecordId provided it will check project-record level access paths (applicant/sponsor) as well.
    /// </summary>
    Task<bool> HasAccessAsync(string userId, string? projectRecordId = null, Guid? modificationId = null);
}