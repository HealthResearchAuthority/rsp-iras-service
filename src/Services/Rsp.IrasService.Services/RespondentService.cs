using Mapster;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

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
            respondentAnswer.UserId = respondentId;

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

        var modificationId = modificationAnswersRequest.ProjectModificationId;
        var projectRecordId = modificationAnswersRequest.ProjectRecordId;
        var userId = modificationAnswersRequest.UserId;

        foreach (var answer in modificationAnswersRequest.ModificationAnswers)
        {
            var respondentAnswer = answer.Adapt<ProjectModificationAnswer>();

            respondentAnswer.ProjectModificationId = modificationId;
            respondentAnswer.ProjectRecordId = projectRecordId;
            respondentAnswer.UserId = userId;

            respondentAnswers.Add(respondentAnswer);
        }

        var specification = new SaveModificationResponsesSpecification(modificationId, projectRecordId, userId);

        await projectPersonnelRepository.SaveModificationResponses(specification, respondentAnswers);
    }

    /// <summary>
    /// Saves respondent answers for a project modification change.
    /// </summary>
    /// <param name="modificationAnswersRequest">The request containing modification answers and identifiers.</param>
    public async Task SaveModificationChangeAnswers(ModificationChangeAnswersRequest modificationAnswersRequest)
    {
        var respondentAnswers = new List<ProjectModificationChangeAnswer>();

        var modificationChangeId = modificationAnswersRequest.ProjectModificationChangeId;
        var projectRecordId = modificationAnswersRequest.ProjectRecordId;
        var userId = modificationAnswersRequest.UserId;

        foreach (var answer in modificationAnswersRequest.ModificationChangeAnswers)
        {
            var respondentAnswer = answer.Adapt<ProjectModificationChangeAnswer>();

            respondentAnswer.ProjectModificationChangeId = modificationChangeId;
            respondentAnswer.ProjectRecordId = projectRecordId;
            respondentAnswer.UserId = userId;

            respondentAnswers.Add(respondentAnswer);
        }

        var specification = new SaveModificationChangeResponsesSpecification(modificationChangeId, projectRecordId);

        await projectPersonnelRepository.SaveModificationChangeResponses(specification, respondentAnswers);
    }

    /// <summary>
    /// Retrieves all respondent answers for a given application.
    /// </summary>
    /// <param name="projectRecordId">The application identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(string projectRecordId)
    {
        var specification = new GetRespondentAnswersSpecification(projectRecordId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a given application and category.
    /// </summary>
    /// <param name="projectRecordId">The application identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(string projectRecordId, string categoryId)
    {
        var specification = new GetRespondentAnswersSpecification(projectRecordId, categoryId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a specific modification and project record.
    /// </summary>
    /// <param name="modificationId">The modification identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationResponses(Guid modificationId, string projectRecordId)
    {
        var specification = new GetModificationAnswersSpecification(modificationId, projectRecordId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a specific modification and project record.
    /// </summary>
    /// <param name="modificationId">The modification identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationResponses(Guid modificationId, string projectRecordId, string categoryId)
    {
        var specification = new GetModificationAnswersSpecification(modificationId, projectRecordId, categoryId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves respondent answers for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationChangeResponses(Guid modificationChangeId, string projectRecordId)
    {
        var specification = new GetModificationChangeAnswersSpecification(modificationChangeId, projectRecordId);

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
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationChangeResponses(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        var specification = new GetModificationChangeAnswersSpecification(modificationChangeId, projectRecordId, categoryId);

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
    /// Retrieves the documents uploaded for a given modification, record, and personnel.
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the project modification.</param>
    /// <param name="projectRecordId">The identifier of the related project record.</param>
    /// <param name="userId">The identifier of the user who uploaded the documents.</param>
    /// <returns>A collection of modification documents as <see cref="ModificationDocumentDto"/>.</returns>
    public async Task<IEnumerable<ModificationDocumentDto>> GetModificationDocumentResponses(Guid projectModificationId, string projectRecordId, string userId)
    {
        var specification = new GetModificationDocumentSpecification(projectModificationId, projectRecordId, userId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<ModificationDocumentDto>>();
    }

    public async Task<ModificationDocumentDto> GetModificationDocumentDetailsResponses(Guid documentId)
    {
        var specification = new GetModificationDocumentDetailsSpecification(documentId);

        var responses = await projectPersonnelRepository.GetResponse(specification);

        return responses.Adapt<ModificationDocumentDto>();
    }

    public async Task<IEnumerable<ModificationDocumentAnswerDto>> GetModificationDocumentAnswersResponses(Guid documentId)
    {
        var specification = new GetModificationDocumentAnswersSpecification(documentId);

        var responses = await projectPersonnelRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<ModificationDocumentAnswerDto>>();
    }

    /// <summary>
    /// Retrieves the list of organisations participating in a specific modification change.
    /// </summary>
    /// <param name="projectModificationChangeId">The identifier of the modification change.</param>
    /// <param name="projectRecordId">The identifier of the project record.</param>
    /// <param name="userId">The identifier of the user who added the organisations.</param>
    /// <returns>A collection of participating organisations as <see cref="ModificationParticipatingOrganisationDto"/>.</returns>
    public async Task<IEnumerable<ModificationParticipatingOrganisationDto>> GetModificationParticipatingOrganisationResponses(Guid projectModificationChangeId, string projectRecordId, string userId)
    {
        var specification = new GetModificationParticipatingOrganisationsSpecification(projectModificationChangeId, projectRecordId, userId);

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
            respondentAnswers[0].ProjectModificationId,
            respondentAnswers[0].ProjectRecordId,
            respondentAnswers[0].UserId
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
    /// Saves the list of document responses associated with a modification change.
    /// </summary>
    /// <param name="respondentAnswer">A list of document DTOs to be saved for the specified modification.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task SaveModificationDocumentAnswerResponses(List<ModificationDocumentAnswerDto> respondentAnswers)
    {
        if (respondentAnswers == null)
        {
            return; // Exit early if null
        }

        // Create the specification based on the first document's identifiers
        var specification = new SaveModificationDocumentAnswerSpecification();

        var respondentDocumentAnswers = new List<ModificationDocumentAnswer>();
        foreach (var answer in respondentAnswers)
        {
            var respondentAnswer = answer.Adapt<ModificationDocumentAnswer>();

            respondentDocumentAnswers.Add(respondentAnswer);
        }

        await projectPersonnelRepository.SaveModificationDocumentAnswerResponses(specification, respondentDocumentAnswers);
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
            respondentAnswers[0].UserId
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

    /// <summary>
    /// Saves the list of document responses associated with a modification change.
    /// </summary>
    /// <param name="respondentAnswers">A list of document DTOs to be saved for the specified modification.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task DeleteModificationDocumentResponses(List<ModificationDocumentDto> respondentAnswers)
    {
        if (respondentAnswers == null || !respondentAnswers.Any())
        {
            return; // Exit early if the list is null or empty
        }

        // Extract all document Ids
        var ids = respondentAnswers.Select(a => a.Id).ToList();

        // Create the specification to fetch all docs + their answers
        var specification = new DeleteModificationDocumentsSpecification(ids);

        // Map DTOs into entities (for deletion tracking in the repo)
        var respondentDocuments = respondentAnswers
            .Select(answer => answer.Adapt<ModificationDocument>())
            .ToList();

        await projectPersonnelRepository.DeleteModificationDocumentResponses(specification, respondentDocuments);
    }

    /// <summary>
    /// Saves the list of document responses associated with a modification change.
    /// </summary>
    /// <param name="documentsAuditTrail">A list of document DTOs to be saved for the specified modification.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task SaveModificationDocumentsAuditTrail(List<ModificationDocumentsAuditTrailDto> documentsAuditTrail)
    {
        if (documentsAuditTrail?.Any() != true)
        {
            return; // Exit early if the list is null or empty
        }

        var modificationDocumentsAuditTrail = new List<ModificationDocumentsAuditTrail>();
        foreach (var answer in documentsAuditTrail)
        {
            var respondentAnswer = answer.Adapt<ModificationDocumentsAuditTrail>();

            modificationDocumentsAuditTrail.Add(respondentAnswer);
        }

        await projectPersonnelRepository.SaveModificationDocumentsAuditTrail(modificationDocumentsAuditTrail);
    }
}