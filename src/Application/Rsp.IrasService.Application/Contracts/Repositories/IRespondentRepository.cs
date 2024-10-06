using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IRespondentRepository
{
    Task<IEnumerable<RespondentAnswer>> GetResponses(ISpecification<RespondentAnswer> specification);

    Task SaveResponses(ISpecification<RespondentAnswer> specification, List<RespondentAnswer> respondentAnswers);
}