using Rsp.QuestionSetService.Domain.Entities;

namespace Rsp.QuestionSetService.Application.Contracts.Repositories;

public interface IRespondentRepository
{
    Task<Respondent> GetByIdAsync(int respondentId);

    Task AddAsync(Respondent respondent);

    Task UpdateAsync(Respondent respondent);
}