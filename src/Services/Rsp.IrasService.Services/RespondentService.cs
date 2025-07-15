using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
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
}