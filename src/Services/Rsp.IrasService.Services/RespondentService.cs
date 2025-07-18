using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

/// <summary>
/// Service for managing respondent answers and modifications for project records.
/// </summary>
public class RespondentService(IProjectPersonnelRepository projectPersonnelRepository) : IRespondentService
{
    /// <summary>
    /// Saves respondent answers for a specific project record.
    /// </summary>
    /// <param name="respondentAnswersRequest">The request containing respondent answers and identifiers.</param>
    public async Task SaveResponses(RespondentAnswersRequest respondentAnswersRequest)
    {
        var respondentAnswers = new List<ProjectRecordAnswer>();

        var projectRecordId = respondentAnswersRequest.ProjectRecordId;
        var respondentId = respondentAnswersRequest.Id;

        foreach (var answer in respondentAnswersRequest.RespondentAnswers)
        {
            var respondentAnswer = answer.Adapt<ProjectRecordAnswer>();
            respondentAnswer.ProjectRecordId = projectRecordId;
            respondentAnswer.ProjectPersonnelId = respondentId;

            respondentAnswers.Add(respondentAnswer);
        }

        var specification = new SaveResponsesSpecification(projectRecordId, respondentId);

        await projectPersonnelRepository.SaveResponses(specification, respondentAnswers);
    }

    /// <summary>
    /// Saves respondent answers for a project modification change.
    /// </summary>
    /// <param name="modificationAnswersRequest">The request containing modification answers and identifiers.</param>
    public async Task SaveModificationAnswers(ModificationAnswersRequest modificationAnswersRequest)
    {
        var respondentAnswers = new List<ProjectModificationAnswer>();

        var modificationChangeId = modificationAnswersRequest.ProjectModificationChangeId;
        var projectRecordId = modificationAnswersRequest.ProjectRecordId;
        var projectPersonnelId = modificationAnswersRequest.ProjectPersonnelId;

        foreach (var answer in modificationAnswersRequest.ModificationAnswers)
        {
            var respondentAnswer = answer.Adapt<ProjectModificationAnswer>();

            respondentAnswer.ProjectModificationChangeId = modificationChangeId;
            respondentAnswer.ProjectRecordId = projectRecordId;
            respondentAnswer.ProjectPersonnelId = projectPersonnelId;

            respondentAnswers.Add(respondentAnswer);
        }

        var specification = new SaveModificationResponsesSpecification(modificationChangeId, projectRecordId, projectPersonnelId);

        await projectPersonnelRepository.SaveModificationResponses(specification, respondentAnswers);
    }

    /// <summary>
    /// Retrieves all respondent answers for a given application.
    /// </summary>
    /// <param name="applicationId">The application identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId)
    {
        var specification = new GetRespondentAnswersSpecification(applicationId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a given application and category.
    /// </summary>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId, string categoryId)
    {
        var specification = new GetRespondentAnswersSpecification(applicationId, categoryId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(Guid modificationChangeId, string projectRecordId)
    {
        var specification = new GetModificationAnswersSpecification(modificationChangeId, projectRecordId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a specific modification change, project record, and category.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        var specification = new GetModificationAnswersSpecification(modificationChangeId, projectRecordId, categoryId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves the list of predefined document types available for selection.
    /// </summary>
    /// <returns>A collection of document types as <see cref="DocumentTypeDto"/>.</returns>
    public async Task<IEnumerable<DocumentTypeResponse>> GetDocumentTypeResponses()
    {
        var specification = new GetDocumentTypesSpecification();

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<DocumentTypeResponse>>();
    }

    /// <summary>
    /// Retrieves the documents uploaded for a given modification change, record, and personnel.
    /// </summary>
    /// <param name="projectModificationChangeId">The unique identifier of the project modification change.</param>
    /// <param name="projectRecordId">The identifier of the related project record.</param>
    /// <param name="projectPersonnelId">The identifier of the project personnel who uploaded the documents.</param>
    /// <returns>A collection of modification documents as <see cref="ModificationDocumentDto"/>.</returns>
    public async Task<IEnumerable<ModificationDocumentDto>> GetModificationDocumentResponses(Guid projectModificationChangeId, string projectRecordId, string projectPersonnelId)
    {
        var specification = new GetModificationDocumentSpecification(projectModificationChangeId, projectRecordId, projectPersonnelId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<ModificationDocumentDto>>();
    }

    /// <summary>
    /// Retrieves the list of organisations participating in a specific modification change.
    /// </summary>
    /// <param name="projectModificationChangeId">The identifier of the modification change.</param>
    /// <param name="projectRecordId">The identifier of the project record.</param>
    /// <param name="projectPersonnelId">The identifier of the project personnel who added the organisations.</param>
    /// <returns>A collection of participating organisations as <see cref="ModificationParticipatingOrganisationDto"/>.</returns>
    public async Task<IEnumerable<ModificationParticipatingOrganisationDto>> GetModificationParticipatingOrganisationResponses(Guid projectModificationChangeId, string projectRecordId, string projectPersonnelId)
    {
        var specification = new GetModificationParticipatingOrganisationsSpecification(projectModificationChangeId, projectRecordId, projectPersonnelId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<ModificationParticipatingOrganisationDto>>();
    }

    /// <summary>
    /// Retrieves the answer details for a specific participating organisation in a modification.
    /// </summary>
    /// <param name="modificationParticipatingOrganisationId">The unique identifier of the modification participating organisation.</param>
    /// <returns>The organisation answer as a <see cref="ModificationParticipatingOrganisationAnswerDto"/>.</returns>
    public async Task<ModificationParticipatingOrganisationAnswerDto> GetModificationParticipatingOrganisationAnswerResponses(Guid modificationParticipatingOrganisationId)
    {
        var specification = new GetModificationParticipatingOrganisationAnswerSpecification(modificationParticipatingOrganisationId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<ModificationParticipatingOrganisationAnswerDto>();
    }

    /// <summary>
    /// Retrieves the list of area of changes and specific area of changes.
    /// </summary>
    /// <returns>A collection of document types as <see cref="ModificationAreaOfChangeDto"/>.</returns>
    public async Task<IEnumerable<ModificationAreaOfChangeDto>> GetModificationAreaOfChanges()
    {
        var specification = new GetModificationAreaOfChangeSpecification();

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<ModificationAreaOfChangeDto>>();
    }

    /// <summary>
    /// Saves the list of document responses associated with a modification change.
    /// </summary>
    /// <param name="respondentAnswers">A list of document DTOs to be saved for the specified modification.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task SaveModificationDocumentResponses(List<ModificationDocumentDto> respondentAnswers)
    {
        if (respondentAnswers == null || !respondentAnswers.Any())
        {
            return; // Exit early if the list is null or empty
        }

        // Create the specification based on the first document's identifiers
        var specification = new SaveModificationDocumentsSpecification(
            respondentAnswers[0].ProjectModificationChangeId,
            respondentAnswers[0].ProjectRecordId,
            respondentAnswers[0].ProjectPersonnelId
        );

        var respondentDocuments = new List<ModificationDocument>();
        foreach (var answer in respondentAnswers)
        {
            var respondentAnswer = answer.Adapt<ModificationDocument>();

            respondentDocuments.Add(respondentAnswer);
        }

        await projectPersonnelRepository.SaveModificationDocumentResponses(specification, respondentDocuments);
    }

    /// <summary>
    /// Saves a list of participating organisations for a specific modification change.
    /// </summary>
    /// <param name="respondentAnswers">A list of organisation DTOs to be saved for the modification change.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task SaveModificationParticipatingOrganisationResponses(List<ModificationParticipatingOrganisationDto> respondentAnswers)
    {
        if (respondentAnswers == null || !respondentAnswers.Any())
        {
            return; // Exit early if the list is null or empty
        }

        // Create the specification based on the first participating organisation's identifiers
        var specification = new SaveModificationParticipatingOrganisationsSpecification(
            respondentAnswers[0].ProjectModificationChangeId,
            respondentAnswers[0].ProjectRecordId,
            respondentAnswers[0].ProjectPersonnelId
        );

        var respondentDocuments = new List<ModificationParticipatingOrganisation>();
        foreach (var answer in respondentAnswers)
        {
            var respondentAnswer = answer.Adapt<ModificationParticipatingOrganisation>();

            respondentDocuments.Add(respondentAnswer);
        }

        await projectPersonnelRepository.SaveModificationParticipatingOrganisationResponses(specification, respondentDocuments);
    }

    /// <summary>
    /// Saves or updates the answer details provided for a specific participating organisation in a modification.
    /// </summary>
    /// <param name="respondentAnswer">The DTO containing answer details for the participating organisation.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task SaveModificationParticipatingOrganisationAnswerResponses(ModificationParticipatingOrganisationAnswerDto respondentAnswer)
    {
        if (respondentAnswer == null)
        {
            return; // Exit early if null
        }

        // Create the specification based on the first participating organisation's identifiers
        var specification = new SaveModificationParticipatingOrganisationAnswerSpecification(
            respondentAnswer.ModificationParticipatingOrganisationId
        );

        var respondentOrganisationAnswer = respondentAnswer.Adapt<ModificationParticipatingOrganisationAnswer>();

        await projectPersonnelRepository.SaveModificationParticipatingOrganisationAnswerResponses(specification, respondentOrganisationAnswer);
    }
}