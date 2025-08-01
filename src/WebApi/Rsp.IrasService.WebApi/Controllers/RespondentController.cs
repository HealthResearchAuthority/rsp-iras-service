using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.WebApi.Controllers;

/// <summary>
/// Controller for handling respondent answers and modification answers.
/// </summary>
[ApiController]
[Route("[controller]")]
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
    /// Saves modification answers for a project modification change.
    /// </summary>
    /// <param name="request">The modification answers request.</param>
    [HttpPost("modification")]
    public async Task SaveModificationAnswers(ModificationAnswersRequest request)
    {
        var command = new SaveModificationAnswersCommand(request);

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
    /// Gets modification answers for a specific modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of respondent answers for the modification.</returns>
    [HttpGet("modification/{modificationChangeId}/{projectRecordId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationAnswers(Guid modificationChangeId, string projectRecordId)
    {
        var command = new GetModificationAnswersQuery
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
    [HttpGet("modification/{modificationChangeId}/{projectRecordId}/{categoryId}")]
    [Produces<IEnumerable<RespondentAnswerDto>>]
    public async Task<IEnumerable<RespondentAnswerDto>> GetModificationAnswers(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        var command = new GetModificationAnswersQuery
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
    /// <param name="modificationChangeId">The modification change identifier.</param>
    /// <param name="projectRecordId">The project record identifier.</param>
    /// <returns>A collection of documents for the modification.</returns>
    [HttpGet("modificationdocument/{modificationChangeId}/{projectRecordId}/{projectPersonnelId}")]
    [Produces<IEnumerable<ModificationDocumentDto>>]
    public async Task<IEnumerable<ModificationDocumentDto>> GetModificationDocuments(Guid modificationChangeId, string projectRecordId, string projectPersonnelId)
    {
        var command = new GetModificationDocumentsQuery
        {
            ProjectModificationChangeId = modificationChangeId,
            ProjectRecordId = projectRecordId,
            ProjectPersonnelId = projectPersonnelId
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
}