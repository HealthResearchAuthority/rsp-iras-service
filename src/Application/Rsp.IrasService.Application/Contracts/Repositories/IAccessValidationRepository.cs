namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IAccessValidationRepository
{
    /// <summary>
    /// Checks if the user has access to the specified project record
    /// </summary>
    /// <param name="userId"> </param>
    /// <param name="projectRecordId"></param>
    Task<bool> HasProjectAccess(string userId, string projectRecordId);

    /// <summary>
    /// <summary>
    /// Checks if the user has access to the specified project record modification
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="projectRecordId"></param>
    /// <param name="modificationId"></param>
    /// <returns></returns>
    Task<bool> HasModificationAccess(string userId, string? projectRecordId, Guid modificationId);

    /// <summary>
    /// <summary>
    /// Checks if the user has access to the specified project record modification
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="projectRecordId"></param>
    /// <param name="modificationId"></param>
    /// <returns></returns>
    Task<bool> HasModificationAccess(string userId, Guid modificationChangeId);

    /// <summary>
    /// Checks if the user has access to the specified document
    /// </summary>
    /// <param name="userId">UserId of the user</param>
    /// <param name="documentId">DocumentId of the document</param>
    Task<bool> HasDocumentAccess(string userId, Guid documentId);
}