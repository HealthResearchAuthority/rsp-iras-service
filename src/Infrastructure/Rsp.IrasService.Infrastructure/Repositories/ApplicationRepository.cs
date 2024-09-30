using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ApplicationRepository(IrasContext irasContext) : IApplicationRepository
{
    public async Task<ResearchApplication> CreateApplication(ResearchApplication irasApplication)
    {
        var entity = await irasContext.ResearchApplications.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ResearchApplication> GetApplication(ISpecification<ResearchApplication> specification)
    {
        var entity = await irasContext
            .ResearchApplications
            .WithSpecification(specification)
            .FirstOrDefaultAsync();

        if (entity != null) return entity;

        // Error handling
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ResearchApplication>> GetApplications(ISpecification<ResearchApplication> specification)
    {
        var result = irasContext
            .ResearchApplications
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<ResearchApplication> UpdateApplication(string applicationId, ResearchApplication irasApplication)
    {
        var entity = await irasContext
            .ResearchApplications
            .FirstOrDefaultAsync(record => record.ApplicationId == applicationId);

        if (entity != null)
        {
            entity.Title = irasApplication.Title;
            //entity.Location = irasApplication.Location;
            //entity.StartDate = irasApplication.StartDate;
            //entity.ApplicationCategories = irasApplication.ApplicationCategories;
            //entity.ProjectCategory = irasApplication.ProjectCategory;
            entity.Status = irasApplication.Status;

            await irasContext.SaveChangesAsync();

            return entity;
        }

        // Error handling
        throw new NotImplementedException();
    }

    //public async Task<IEnumerable<QuestionDto>> GetNextQuestions(string categoryId, IEnumerable<RespondentAnswer> respondentAnswers)
    //{
    //    var nextQuestions = await questionRepository.GetQuestions(categoryId);

    //    var questions = new List<Question>();

    //    foreach (var question in nextQuestions)
    //    {
    //        if (question.QuestionRules.Count == 0)
    //        {
    //            questions.Add(question);
    //            continue;
    //        }

    //        // get the question conditions
    //        var conditions = question.QuestionRules.Where(c => c.QuestionId == question.QuestionId);

    //        // check for each condition that the parent question answer
    //        // matches the option for the condition.

    //        var satisfiedConditions = 0;

    //        // get the respondent answer for question's parent question
    //        foreach (var condition in conditions)
    //        {
    //            var answer = respondentAnswers.FirstOrDefault(answer => answer.QuestionId == condition.ParentQuestionId);

    //            if (answer == null)
    //            {
    //                continue;
    //            }

    //            // answer option matches the condition option
    //            if (answer.OptionId == condition.ParentOptionId)
    //            {
    //                satisfiedConditions++;
    //            }
    //        }

    //        // all conditions are met
    //        if (satisfiedConditions == conditions.Count())
    //        {
    //            questions.Add(question);
    //        }
    //    }

    //    return questions.Adapt<IEnumerable<QuestionDto>>();
    //}
}