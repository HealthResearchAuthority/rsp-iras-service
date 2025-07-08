using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class RespondentService(IProjectPersonnelRepository respondentRepository) : IRespondentService
{
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

        await respondentRepository.SaveResponses(specification, respondentAnswers);
    }

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

        await respondentRepository.SaveModificationResponses(specification, respondentAnswers);
    }

    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId)
    {
        var specification = new GetRespondentAnswersSpecification(applicationId);

        var responses = await respondentRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(string applicationId, string categoryId)
    {
        var specification = new GetRespondentAnswersSpecification(applicationId, categoryId);

        var responses = await respondentRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(Guid modificationChangeId, string projectRecordId)
    {
        var specification = new GetModificationAnswersSpecification(modificationChangeId, projectRecordId);

        var responses = await respondentRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }

    public async Task<IEnumerable<RespondentAnswerDto>> GetResponses(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        var specification = new GetModificationAnswersSpecification(modificationChangeId, projectRecordId, categoryId);

        var responses = await respondentRepository.GetResponses(specification);

        return responses.Adapt<IEnumerable<RespondentAnswerDto>>();
    }
}