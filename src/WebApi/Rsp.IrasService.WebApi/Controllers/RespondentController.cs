using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.WebApi.Controllers;

/// <summary>
/// Controller for handling respondent answers and modification answers.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class RespondentController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Saves respondent answers for a project record.
    /// </summary>
    /// <param name="request">The respondent answers request.</param>
    [HttpPost]
    public async Task SaveRespondentAnswers(RespondentAnswersRequest request)
    {
        var command = new SaveResponsesCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification answers for a project modification.
    /// </summary>
    /// <param name="request">The modification answers request.</param>
    [HttpPost("modification")]
    public async Task SaveModificationAnswers(ModificationAnswersRequest request)
    {
        var command = new SaveModificationAnswersCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification answers for a project modification change.
    /// </summary>
    /// <param name="request">The modification change answers request.</param>
    [HttpPost("modificationchange")]
    public async Task SaveModificationChangeAnswers(ModificationChangeAnswersRequest request)
    {
        var command = new SaveModificationChangeAnswersCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification participating organisations for a project modification change.
    /// </summary>
    /// <param name="request">The modification participating organisations request.</param>
    [HttpPost("modificationparticipatingorganisation")]
    public async Task SaveModificationParticipatingOrganisations(List<ModificationParticipatingOrganisationDto> request)
    {
        var command = new SaveModificationParticipatingOrganisationsCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification participating organisation answers for a project modification change.
    /// </summary>
    /// <param name="request">The modification participating organisation answers request.</param>
    [HttpPost("modificationparticipatingorganisationanswer")]
    public async Task SaveModificationParticipatingOrganisationAnswers(ModificationParticipatingOrganisationAnswerDto request)
    {
        var command = new SaveModificationParticipatingOrganisationAnswersCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification participating organisation answers for a project modification change.
    /// </summary>
    /// <param name="request">The modification participating organisation answers request.</param>
    [HttpPost("modificationdocumentanswer")]
    public async Task SaveModificationDocumentAnswers(List<ModificationDocumentAnswerDto> request)
    {
        var command = new SaveModificationDocumentAnswersCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Saves modification documents for a project modification change.
    /// </summary>
    /// <param name="request">The modification documents request.</param>
    [HttpPost("modificationdocument")]
    public async Task SaveModificationDocuments(List<ModificationDocumentDto> request)
    {
        var command = new SaveModificationDocumentsCommand(request);

        await mediator.Send(command);
    }

    /// <summary>
    /// Gets respondent answers for a specific application.
    /// </summary>
    /// <param name="applicationId">The application identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    [HttpGet("{applicationId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetRespondentAnswers(string applicationId)
    {
        var command = new GetResponsesQuery
        {
            ApplicationId = applicationId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets respondent answers for a specific application and category.
    /// </summary>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers.</returns>
    [HttpGet("{applicationId}/{categoryId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetRespondentAnswers(string applicationId, string categoryId)
    {
        var command = new GetResponsesQuery
        {
            ApplicationId = applicationId,
            CategoryId = categoryId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification answers for a specific modification and project record.
    /// </summary>
    /// <param name="modificationId">The modification identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers for the modification.</returns>
    [HttpGet("modification/{modificationId}/{projectRecordId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationAnswers(Guid modificationId, string projectRecordId)
    {
        var command = new GetModificationAnswersQuery
        {
            ProjectModificationId = modificationId,
            ProjectRecordId = projectRecordId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification answers for a specific modification change, project record, and category.
    /// </summary>
    /// <param name="modificationId">The modification identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers for the modification and category.</returns>
    [HttpGet("modification/{modificationId}/{projectRecordId}/{categoryId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationAnswers(Guid modificationId, string projectRecordId, string categoryId)
    {
        var command = new GetModificationAnswersQuery
        {
            ProjectModificationId = modificationId,
            ProjectRecordId = projectRecordId,
            CategoryId = categoryId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification answers for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers for the modification.</returns>
    [HttpGet("modificationchange/{modificationChangeId}/{projectRecordId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationChangeAnswers(Guid modificationChangeId, string projectRecordId)
    {
        var command = new GetModificationChangeAnswersQuery
        {
            ProjectModificationChangeId = modificationChangeId,
            ProjectRecordId = projectRecordId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification answers for a specific modification change, project record, and category.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A collection of respondent answers for the modification and category.</returns>
    [HttpGet("modificationchange/{modificationChangeId}/{projectRecordId}/{categoryId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationChangeAnswers(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        var command = new GetModificationChangeAnswersQuery
        {
            ProjectModificationChangeId = modificationChangeId,
            ProjectRecordId = projectRecordId,
            CategoryId = categoryId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Returns all document types
    /// </summary>
    [HttpGet]
    [Produces<IEnumerable<DocumentType>>]
    public async Task<IEnumerable<DocumentTypeResponse>> GetDocumentTypes()
    {
        var query = new GetDocumentTypesQuery();

        return await mediator.Send(query);
    }

    /// <summary>
    /// Gets modification documents for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of documents for the modification.</returns>
    [HttpGet("modificationdocument/{modificationId}/{projectRecordId}")]
    [Produces<IEnumerable<ModificationDocumentDto>>]
    public async Task<IEnumerable<ModificationDocumentDto>> GetModificationDocuments(Guid modificationId, string projectRecordId)
    {
        var command = new GetModificationDocumentsQuery
        {
            ProjectModificationId = modificationId,
            ProjectRecordId = projectRecordId,
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification documents for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of documents for the modification.</returns>
    [HttpGet("modificationdocument/{modificationId}/{projectRecordId}/{userId}")]
    [Produces<IEnumerable<ModificationDocumentDto>>]
    public async Task<IEnumerable<ModificationDocumentDto>> GetModificationDocuments(Guid modificationId, string projectRecordId, string userId)
    {
        var command = new GetModificationDocumentsQuery
        {
            ProjectModificationId = modificationId,
            ProjectRecordId = projectRecordId,
            UserId = userId
        };

        return await mediator.Send(command);
    }

    [HttpGet("modificationdocumentdetails/{documentId}")]
    [Produces<ModificationDocumentDto>]
    public async Task<ModificationDocumentDto> GetModificationDocumentDetails(Guid documentId)
    {
        var command = new GetModificationDocumentDetailsQuery
        {
            Id = documentId
        };

        return await mediator.Send(command);
    }

    [HttpGet("modificationdocumentanswer/{documentId}")]
    [Produces<ModificationDocumentAnswerDto>]
    public async Task<IEnumerable<ModificationDocumentAnswerDto>> GetModificationDocumentAnswers(Guid documentId)
    {
        var command = new GetModificationDocumentAnswersQuery
        {
            Id = documentId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification participating organisations for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of documents for the modification.</returns>
    [HttpGet("modificationparticipatingorganisation/{modificationChangeId}/{projectRecordId}")]
    [Produces<IEnumerable<ModificationParticipatingOrganisationDto>>]
    public async Task<IEnumerable<ModificationParticipatingOrganisationDto>> GetModificationParticipatingOrganisations(Guid modificationChangeId, string projectRecordId)
    {
        var command = new GetModificationParticipatingOrganisationsQuery
        {
            ProjectModificationChangeId = modificationChangeId,
            ProjectRecordId = projectRecordId
        };

        return await mediator.Send(command);
    }

    /// <summary>
    /// Gets modification participating organisation answer for a specific organisation.
    /// </summary>
    /// <param name="participatingOrganisationId">The participating organisation identifier.</param>
    /// <returns>A participating organisation answer for the participating organisation.</returns>
    [HttpGet("modificationparticipatingorganisationanswer/{participatingOrganisationId}")]
    [Produces<ModificationParticipatingOrganisationAnswerDto>]
    public async Task<ModificationParticipatingOrganisationAnswerDto> GetModificationParticipatingOrganisationAnswer(Guid participatingOrganisationId)
    {
        var command = new GetModificationParticipatingOrganisationAnswerQuery
        {
            ModificationParticipatingOrganisationId = participatingOrganisationId
        };

        return await mediator.Send(command);
    }

    [HttpGet("modificationdocumentbytype/{projectRecordId}")]
    [Produces<IEnumerable<ModificationDocumentDto>>]
    public async Task<IEnumerable<ModificationDocumentDto>> GetModificationDocumentsByType(string projectRecordId, [FromQuery] string? documentTypeId)
    {
        var command = new GetModificationDocumentsByTypeQuery
        {
            ProjectRecordId = projectRecordId,
            DocumentTypeId = documentTypeId
        };

        return await mediator.Send(command);
    }
}