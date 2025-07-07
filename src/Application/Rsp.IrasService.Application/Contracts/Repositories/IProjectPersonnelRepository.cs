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
}