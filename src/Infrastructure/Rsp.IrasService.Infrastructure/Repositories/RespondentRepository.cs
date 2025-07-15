using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

/// <summary>
/// Repository for managing project personnel responses and modification responses.
/// </summary>
public class RespondentRepository(IrasContext irasContext) : IProjectPersonnelRepository
{
    /// <summary>
    /// Saves the provided project record answers that match the given specification.
    /// Updates existing answers, removes cleared answers, or adds new answers as appropriate.
    /// </summary>
    /// <param name="specification">The specification to filter which answers to save.</param>
    /// <param name="respondentAnswers">The list of answers to save.</param>
    public async Task SaveResponses(ISpecification<ProjectRecordAnswer> specification, List<ProjectRecordAnswer> respondentAnswers)
    {
        var answers = irasContext
            .ProjectRecordAnswers
            .WithSpecification(specification);

        foreach (var answer in respondentAnswers)
        {
            var existingAnswer = answers.FirstOrDefault(ans => ans.QuestionId == answer.QuestionId);

            if (existingAnswer != null)
            {
                // Delete the answer if it was previously saved and is now cleared or all options are unselected
                if ((string.IsNullOrWhiteSpace(existingAnswer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                    (existingAnswer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
                {
                    irasContext.ProjectRecordAnswers.Remove(existingAnswer);
                    continue;
                }

                // Update the existing answer
                existingAnswer.Response = answer.Response;
                existingAnswer.SelectedOptions = answer.SelectedOptions;

                continue;
            }

            // Do not add if answer is multiple choice but none of the options are selected
            if ((string.IsNullOrWhiteSpace(answer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                (answer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
            {
                continue;
            }

            // Add new answer
            await irasContext.ProjectRecordAnswers.AddAsync(answer);
        }

        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Saves the provided project modification answers that match the given specification.
    /// Updates existing answers, removes cleared answers, or adds new answers as appropriate.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to save.</param>
    /// <param name="respondentAnswers">The list of modification answers to save.</param>
    public async Task SaveModificationResponses(ISpecification<ProjectModificationAnswer> specification, List<ProjectModificationAnswer> respondentAnswers)
    {
        var answers = irasContext
            .ProjectModificationAnswers
            .WithSpecification(specification);

        foreach (var answer in respondentAnswers)
        {
            var existingAnswer = answers.FirstOrDefault(ans => ans.QuestionId == answer.QuestionId);

            if (existingAnswer != null)
            {
                // Delete the answer if it was previously saved and is now cleared or all options are unselected
                if ((string.IsNullOrWhiteSpace(existingAnswer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                    (existingAnswer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
                {
                    irasContext.ProjectModificationAnswers.Remove(existingAnswer);
                    continue;
                }

                // Update the existing answer
                existingAnswer.Response = answer.Response;
                existingAnswer.SelectedOptions = answer.SelectedOptions;

                continue;
            }

            // Do not add if answer is multiple choice but none of the options are selected
            if ((string.IsNullOrWhiteSpace(answer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                (answer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
            {
                continue;
            }

            // Add new answer
            await irasContext.ProjectModificationAnswers.AddAsync(answer);
        }

        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves project record answers matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project record answers.</param>
    /// <returns>A collection of <see cref="ProjectRecordAnswer"/> objects.</returns>
    public Task<IEnumerable<ProjectRecordAnswer>> GetResponses(ISpecification<ProjectRecordAnswer> specification)
    {
        var result = irasContext
           .ProjectRecordAnswers
           .WithSpecification(specification)
           .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Retrieves project modification answers matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project modification answers.</param>
    /// <returns>A collection of <see cref="ProjectModificationAnswer"/> objects.</returns>
    public Task<IEnumerable<ProjectModificationAnswer>> GetResponses(ISpecification<ProjectModificationAnswer> specification)
    {
        var result = irasContext
           .ProjectModificationAnswers
           .WithSpecification(specification)
           .AsEnumerable();

        return Task.FromResult(result);
    }
}