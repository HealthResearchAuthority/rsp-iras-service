using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts;

public interface IRespondentRepository
{
    Task<Respondent> GetByIdAsync(int respondentId);

    Task AddAsync(Respondent respondent);

    Task UpdateAsync(Respondent respondent);
}