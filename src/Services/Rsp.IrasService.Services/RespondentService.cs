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

        var applicationId = respondentAnswersRequest.ProjectApplicationId;
        var respondentId = respondentAnswersRequest.Id;

        foreach (var answer in respondentAnswersRequest.RespondentAnswers)
        {
            var respondentAnswer = answer.Adapt<ProjectRecordAnswer>();
            respondentAnswer.ProjectRecordId = applicationId;
            respondentAnswer.ProjectPersonnelId = respondentId;

            respondentAnswers.Add(respondentAnswer);
        }

        var specification = new SaveResponsesSpecification(applicationId, respondentId);

        await respondentRepository.SaveResponses(specification, respondentAnswers);
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
}