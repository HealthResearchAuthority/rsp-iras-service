using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Enums;
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
            var existingAnswer = await answers.FirstOrDefaultAsync(ans => ans.QuestionId == answer.QuestionId);

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
                existingAnswer.OptionType = answer.OptionType;
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
    /// Saves the provided project modification answers that match the given specification.
    /// Updates existing answers, removes cleared answers, or adds new answers as appropriate.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to save.</param>
    /// <param name="respondentAnswers">The list of modification answers to save.</param>
    public async Task SaveModificationChangeResponses(ISpecification<ProjectModificationChangeAnswer> specification, List<ProjectModificationChangeAnswer> respondentAnswers)
    {
        var answers = irasContext
            .ProjectModificationChangeAnswers
            .WithSpecification(specification);

        foreach (var answer in respondentAnswers)
        {
            var existingAnswer = await answers.FirstOrDefaultAsync(ans => ans.QuestionId == answer.QuestionId);

            if (existingAnswer != null)
            {
                // Delete the answer if it was previously saved and is now cleared or all options are unselected
                if ((string.IsNullOrWhiteSpace(existingAnswer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                    (existingAnswer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
                {
                    irasContext.ProjectModificationChangeAnswers.Remove(existingAnswer);
                    continue;
                }

                // Update the existing answer
                existingAnswer.OptionType = answer.OptionType;
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
            await irasContext.ProjectModificationChangeAnswers.AddAsync(answer);
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
    /// <returns>A collection of <see cref="ProjectModificationChangeAnswer"/> objects.</returns>
    public Task<IEnumerable<ProjectModificationAnswer>> GetResponses(ISpecification<ProjectModificationAnswer> specification)
    {
        var result = irasContext
           .ProjectModificationAnswers
           .WithSpecification(specification)
           .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Retrieves project modification change answers matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter project modification answers.</param>
    /// <returns>A collection of <see cref="ProjectModificationChangeAnswer"/> objects.</returns>
    public Task<IEnumerable<ProjectModificationChangeAnswer>> GetResponses(ISpecification<ProjectModificationChangeAnswer> specification)
    {
        var result = irasContext
           .ProjectModificationChangeAnswers
           .WithSpecification(specification)
           .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets document types based on the provided specification.
    /// </summary>
    /// <param name="specification">The specification to filter document type.</param>
    /// <returns>A collection of <see cref="DocumentType"/> objects.</returns>
    public Task<IEnumerable<DocumentType>> GetResponses(ISpecification<DocumentType> specification)
    {
        var result = irasContext
            .DocumentTypes
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets modification documents matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter modfication documents.</param>
    /// <returns>A collection of <see cref="ModificationDocument"/> objects.</returns>
    public Task<IEnumerable<ModificationDocument>> GetResponses(ISpecification<ModificationDocument> specification)
    {
        var result = irasContext
            .ModificationDocuments
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets modification documents matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter modification documents.</param>
    /// <returns>A collection of <see cref="ModificationDocument"/> objects.</returns>
    public Task<ModificationDocument> GetResponse(ISpecification<ModificationDocument> specification)
    {
        var result = irasContext
            .ModificationDocuments
            .WithSpecification(specification)
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets participating organisations for a modification based on specification.
    /// </summary>
    /// <param name="specification">The specification to filter modification participating organisations.</param>
    /// <returns>A collection of <see cref="ModificationParticipatingOrganisation"/> objects.</returns>
    public Task<IEnumerable<ModificationParticipatingOrganisation>> GetResponses(ISpecification<ModificationParticipatingOrganisation> specification)
    {
        var result = irasContext
            .ModificationParticipatingOrganisations
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets a single participating organisation answer matching the given specification.
    /// </summary>
    /// <param name="specification">The specification to filter modification participating organisation answers.</param>
    /// <returns>A collection of <see cref="ModificationParticipatingOrganisationAnswer"/> objects.</returns>
    public Task<ModificationParticipatingOrganisationAnswer> GetResponses(ISpecification<ModificationParticipatingOrganisationAnswer> specification)
    {
        var result = irasContext
            .ModificationParticipatingOrganisationAnswers
            .WithSpecification(specification)
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Saves a list of document responses. Updates if existing, adds if new.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to save.</param>
    /// <param name="respondentAnswers">The list of modification answers to save.</param>
    public async Task SaveModificationDocumentResponses(
        ISpecification<ModificationDocument> specification,
        List<ModificationDocument> respondentAnswers)
    {
        var documents = irasContext
            .ModificationDocuments
            .WithSpecification(specification);

        foreach (var answer in respondentAnswers)
        {
            var existingAnswer = documents.FirstOrDefault(ans => ans.Id == answer.Id);

            if (existingAnswer != null)
            {
                // Update existing document entry
                existingAnswer.ProjectModificationChangeId = answer.ProjectModificationChangeId;
                existingAnswer.ProjectRecordId = answer.ProjectRecordId;
                existingAnswer.ProjectPersonnelId = answer.ProjectPersonnelId;
                existingAnswer.FileName = answer.FileName;
                existingAnswer.DocumentStoragePath = answer.DocumentStoragePath;
                existingAnswer.FileSize = answer.FileSize;
                existingAnswer.Status = answer.Status;

                continue;
            }

            // Add new document entry
            await irasContext.ModificationDocuments.AddAsync(answer);
        }

        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Saves participating organisations linked to a modification. Updates if existing, adds if new.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to save.</param>
    /// <param name="respondentAnswers">The list of modification answers to save.</param>
    public async Task SaveModificationParticipatingOrganisationResponses(
        ISpecification<ModificationParticipatingOrganisation> specification,
        List<ModificationParticipatingOrganisation> respondentAnswers)
    {
        var organisations = irasContext
            .ModificationParticipatingOrganisations
            .WithSpecification(specification);

        foreach (var answer in respondentAnswers)
        {
            var existingAnswer = organisations.FirstOrDefault(ans => ans.Id == answer.Id);

            if (existingAnswer != null)
            {
                // Update organisation entry
                existingAnswer.ProjectModificationChangeId = answer.ProjectModificationChangeId;
                existingAnswer.ProjectRecordId = answer.ProjectRecordId;
                existingAnswer.ProjectPersonnelId = answer.ProjectPersonnelId;
                existingAnswer.OrganisationId = answer.OrganisationId;

                continue;
            }

            // Add new organisation entry
            await irasContext.ModificationParticipatingOrganisations.AddAsync(answer);
        }

        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Saves or updates a single answer for a participating organisation in a modification.
    /// Handles delete logic if empty or deselected.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to save.</param>
    /// <param name="respondentAnswer">The modification answer to save.</param>
    public async Task SaveModificationParticipatingOrganisationAnswerResponses(
        ISpecification<ModificationParticipatingOrganisationAnswer> specification,
        ModificationParticipatingOrganisationAnswer respondentAnswer)
    {
        var organisations = irasContext
            .ModificationParticipatingOrganisationAnswers
            .WithSpecification(specification);

        var existingAnswer = organisations.FirstOrDefault(ans => ans.Id == respondentAnswer.Id);

        if (existingAnswer != null)
        {
            // Delete if answer is empty or options are deselected
            if ((string.IsNullOrWhiteSpace(existingAnswer.OptionType) && string.IsNullOrWhiteSpace(respondentAnswer.Response)) ||
                (existingAnswer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(respondentAnswer.SelectedOptions)))
            {
                irasContext.ModificationParticipatingOrganisationAnswers.Remove(existingAnswer);
            }
            else
            {
                // Update answer
                existingAnswer.Response = respondentAnswer.Response;
                existingAnswer.SelectedOptions = respondentAnswer.SelectedOptions;
            }
        }
        else
        {
            // Add new answer
            await irasContext.ModificationParticipatingOrganisationAnswers.AddAsync(respondentAnswer);
        }

        await irasContext.SaveChangesAsync();
    }

    public Task<IEnumerable<ModificationDocumentAnswer>> GetResponses(ISpecification<ModificationDocumentAnswer> specification)
    {
        var result = irasContext
            .ModificationDocumentAnswers
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task SaveModificationDocumentAnswerResponses(
    ISpecification<ModificationDocumentAnswer> specification,
    List<ModificationDocumentAnswer> respondentAnswers)
    {
        // Query tracked entities directly (no .ToList() yet)
        var existingAnswersQuery = irasContext
            .ModificationDocumentAnswers
            .WithSpecification(specification);

        foreach (var respondentAnswer in respondentAnswers)
        {
            // Try to find existing by Id (ignore if Guid.Empty)
            ModificationDocumentAnswer? existingAnswer = null;
            if (respondentAnswer.Id != Guid.Empty)
            {
                existingAnswer = await existingAnswersQuery
                    .FirstOrDefaultAsync(ans => ans.Id == respondentAnswer.Id);
            }

            if (existingAnswer != null)
            {
                // Delete if empty
                if (string.IsNullOrWhiteSpace(respondentAnswer.Response) &&
                    string.IsNullOrWhiteSpace(respondentAnswer.SelectedOptions))
                {
                    irasContext.ModificationDocumentAnswers.Remove(existingAnswer);
                    continue;
                }

                // Update tracked entity
                existingAnswer.Response = respondentAnswer.Response;
                existingAnswer.SelectedOptions = respondentAnswer.SelectedOptions;
            }
            else
            {
                // Skip insert if no values
                if (string.IsNullOrWhiteSpace(respondentAnswer.Response) &&
                    string.IsNullOrWhiteSpace(respondentAnswer.SelectedOptions))
                {
                    continue;
                }

                // Insert new
                await irasContext.ModificationDocumentAnswers.AddAsync(respondentAnswer);
            }
        }

        // Save to DB
        await irasContext.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a list of documents.
    /// </summary>
    /// <param name="specification">The specification to filter which modification answers to delete.</param>
    /// <param name="respondentAnswers">The list of modification answers to delete.</param>
    public async Task DeleteModificationDocumentResponses(
    ISpecification<ModificationDocument> specification,
    List<ModificationDocument> respondentAnswers)
    {
        // Fetch all matching documents (with answers included via specification)
        var documents = await irasContext
            .ModificationDocuments
            .WithSpecification(specification)
            .ToListAsync();

        foreach (var doc in documents)
        {
            // Only delete if status is NOT "SubmittedToSponsor"
            if (!string.Equals(doc.Status, DocumentStatus.SubmittedToSponsor.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // Remove related answers first
                var relatedAnswers = await irasContext.ModificationDocumentAnswers
                .Where(a => a.ModificationDocumentId == doc.Id)
                .ToListAsync();

                if (relatedAnswers.Any())
                {
                    irasContext.ModificationDocumentAnswers.RemoveRange(relatedAnswers);
                }

                // Remove the document itself
                irasContext.ModificationDocuments.Remove(doc);
            }
        }

        await irasContext.SaveChangesAsync();
    }
}