using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

/// <summary>
/// Repository interface for managing project personnel responses and modifications.
/// </summary>
public interface IProjectPersonnelRepository
{
    /// <summary>
    /// Retrieves project record answers matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project record answers.</param>
    /// <returns>A collection of <see cref="ProjectRecordAnswer"/> objects.</returns>
    Task<IEnumerable<ProjectRecordAnswer>> GetResponses(ISpecification<ProjectRecordAnswer> specification);

    /// <summary>
    /// Retrieves project modification answers matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project modification answers.</param>
    /// <returns>A collection of <see cref="ProjectModificationAnswer"/> objects.</returns>
    Task<IEnumerable<ProjectModificationAnswer>> GetResponses(ISpecification<ProjectModificationAnswer> specification);

    /// <summary>
    /// Retrieves document types matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project document types.</param>
    /// <returns>A collection of <see cref="DocumentType"/> objects.</returns>
    Task<IEnumerable<DocumentType>> GetResponses(ISpecification<DocumentType> specification);

    /// <summary>
    /// Retrieves modification documents matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project modification documents.</param>
    /// <returns>A collection of <see cref="ModificationDocument"/> objects.</returns>
    Task<IEnumerable<ModificationDocument>> GetResponses(ISpecification<ModificationDocument> specification);

    /// <summary>
    /// Retrieves modification participating organisations matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project modification documents.</param>
    /// <returns>A collection of <see cref="ModificationParticipatingOrganisation"/> objects.</returns>
    Task<IEnumerable<ModificationParticipatingOrganisation>> GetResponses(ISpecification<ModificationParticipatingOrganisation> specification);

    /// <summary>
    /// Retrieves modification participating organisation answer matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project modification documents.</param>
    /// <returns>A <see cref="ModificationParticipatingOrganisationAnswer"/> object.</returns>
    Task<ModificationParticipatingOrganisationAnswer> GetResponses(ISpecification<ModificationParticipatingOrganisationAnswer> specification);

    /// <summary>
    /// Saves the provided project record answers that match the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter which answers to save.</param>
    /// <param name="respondentAnswers">The list of answers to save.</param>
    Task SaveResponses(ISpecification<ProjectRecordAnswer> specification, List<ProjectRecordAnswer> respondentAnswers);

    /// <summary>
    /// Saves the provided project modification answers that match the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to save.</param>
    /// <param name="respondentAnswers">The list of modification answers to save.</param>
    Task SaveModificationResponses(ISpecification<ProjectModificationAnswer> specification, List<ProjectModificationAnswer> respondentAnswers);

    /// <summary>
    /// Saves the provided project modification documents that match the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter which modification documents to save.</param>
    /// <param name="respondentAnswers">The list of modification documents to save.</param>
    Task SaveModificationDocumentResponses(ISpecification<ModificationDocument> specification, List<ModificationDocument> respondentAnswers);

    /// <summary>
    /// Saves the provided modification participating oraganisations that match the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter which modification participating oraganisations to save.</param>
    /// <param name="respondentAnswers">The list of modification participating oraganisations to save.</param>
    Task SaveModificationParticipatingOrganisationResponses(ISpecification<ModificationParticipatingOrganisation> specification, List<ModificationParticipatingOrganisation> respondentAnswers);

    /// <summary>
    /// Saves the provided modification participating oraganisation answer that match the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter which modification participating oraganisation answer to save.</param>
    /// <param name="respondentAnswer">The modification participating oraganisation answer to save.</param>
    Task SaveModificationParticipatingOrganisationAnswerResponses(ISpecification<ModificationParticipatingOrganisationAnswer> specification, ModificationParticipatingOrganisationAnswer respondentAnswer);
}