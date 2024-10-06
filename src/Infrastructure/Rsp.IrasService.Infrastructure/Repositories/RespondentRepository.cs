using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class RespondentRepository(IrasContext irasContext) : IRespondentRepository
{
    public async Task SaveResponses(ISpecification<RespondentAnswer> specification, List<RespondentAnswer> respondentAnswers)
    {
        var answers = irasContext
            .RespondentAnswers
            .WithSpecification(specification);

        foreach (var answer in respondentAnswers)
        {
            var existingAnswer = answers.FirstOrDefault(ans => ans.QuestionId == answer.QuestionId);

            if (existingAnswer != null)
            {
                // delete the answer if existingAnswer was previously saved
                // and now cleared or all of the options are unselected
                if ((string.IsNullOrWhiteSpace(existingAnswer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                    (existingAnswer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
                {
                    irasContext.RespondentAnswers.Remove(existingAnswer);
                    continue;
                }

                existingAnswer.Response = answer.Response;
                existingAnswer.SelectedOptions = answer.SelectedOptions;

                continue;
            }

            // do not add if answer is multiplechoice but none of the options are selected
            if ((string.IsNullOrWhiteSpace(answer.OptionType) && string.IsNullOrWhiteSpace(answer.Response)) ||
                (answer.OptionType is "Single" or "Multiple" && string.IsNullOrWhiteSpace(answer.SelectedOptions)))
            {
                continue;
            }

            await irasContext.RespondentAnswers.AddAsync(answer);
        }

        await irasContext.SaveChangesAsync();
    }

    public Task<IEnumerable<RespondentAnswer>> GetResponses(ISpecification<RespondentAnswer> specification)
    {
        var result = irasContext
           .RespondentAnswers
           .WithSpecification(specification)
           .AsEnumerable();

        return Task.FromResult(result);
    }
}