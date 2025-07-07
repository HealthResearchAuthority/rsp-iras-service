using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create, read, and update respondent project records in the database.
/// Marked as IInterceptable to enable start/end logging for all methods.
/// </summary>
public interface IRespondentService : IInterceptable
{
    /// <summary>
    /// Retrieves all respondent answers for a given project record.
    /// </summary>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    Task<IEnumerable<RespondentAnswerDto>> GetResponses(string projectRecordId);

    /// <summary>
    /// Retrieves respondent answers for a given project record and category.
    /// </summary>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    Task<IEnumerable<RespondentAnswerDto>> GetResponses(string projectRecordId, string categoryId);

    /// <summary>
    /// Retrieves respondent answers for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    Task<IEnumerable<RespondentAnswerDto>> GetResponses(Guid modificationChangeId, string projectRecordId);

    /// <summary>
    /// Retrieves respondent answers for a specific modification change, project record, and category.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    Task<IEnumerable<RespondentAnswerDto>> GetResponses(Guid modificationChangeId, string projectRecordId, string categoryId);

    /// <summary>
    /// Saves respondent answers for a project modification.
    /// </summary>
    /// <param name="modificationAnswersRequest">The modification answers request containing answers to save.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveModificationAnswers(ModificationAnswersRequest modificationAnswersRequest);

    /// <summary>
    /// Saves respondent answers for an project record.
    /// </summary>
    /// <param name="respondentAnswersRequest">The respondent answers request containing answers to save.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveResponses(RespondentAnswersRequest respondentAnswersRequest);
}