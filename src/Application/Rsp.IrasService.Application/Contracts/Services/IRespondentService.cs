using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
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
    /// Retrieves document types matching the given specification.
    /// </summary>
    /// <returns>A collection of <see cref="DocumentType"/> objects.</returns>
    Task<IEnumerable<DocumentTypeResponse>> GetDocumentTypeResponses();

    /// <summary>
    /// Retrieves modification documents matching the given specification.
    /// </summary>
    /// <param name="projectModificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="projectPersonnelId">The project personnel identifier.</param>
    /// <returns>A collection of <see cref="ModificationDocument"/> objects.</returns>
    Task<IEnumerable<ModificationDocumentDto>> GetModificationDocumentResponses(Guid projectModificationChangeId, string projectRecordId, string projectPersonnelId);

    /// <summary>
    /// Retrieves modification participating organisations matching the given specification.
    /// </summary>
    /// <param name="projectModificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="projectPersonnelId">The project personnel identifier.</param>
    /// <returns>A collection of <see cref="ModificationParticipatingOrganisationDto"/> objects.</returns>
    Task<IEnumerable<ModificationParticipatingOrganisationDto>> GetModificationParticipatingOrganisationResponses(Guid projectModificationChangeId, string projectRecordId, string projectPersonnelId);

    /// <summary>
    /// Retrieves modification participating organisation answer matching the given specification.
    /// </summary>
    /// <param name="modificationParticipatingOrganisationId">The modification participating organisation identifier.</param>
    /// <returns>A <see cref="ModificationParticipatingOrganisationAnswerDto"/> object.</returns>
    Task<ModificationParticipatingOrganisationAnswerDto> GetModificationParticipatingOrganisationAnswerResponses(Guid modificationParticipatingOrganisationId);

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

    /// <summary>
    /// Saves the provided project modification documents that match the given specification.
    /// </summary>
    /// <param name="respondentAnswers">The list of modification documents to save.</param>
    Task SaveModificationDocumentResponses(List<ModificationDocumentDto> respondentAnswers);

    /// <summary>
    /// Saves the provided modification participating oraganisations that match the given specification.
    /// </summary>
    /// <param name="respondentAnswers">The list of modification participating oraganisations to save.</param>
    Task SaveModificationParticipatingOrganisationResponses(List<ModificationParticipatingOrganisationDto> respondentAnswers);

    /// <summary>
    /// Saves the provided modification participating oraganisation answer that match the given specification.
    /// </summary>
    /// <param name="respondentAnswer">The modification participating oraganisation answer to save.</param>
    Task SaveModificationParticipatingOrganisationAnswerResponses(ModificationParticipatingOrganisationAnswerDto respondentAnswer);
}