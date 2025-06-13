using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IRespondentRepository
{
    Task<IEnumerable<ProjectApplicationRespondentAnswer>> GetResponses(ISpecification<ProjectApplicationRespondentAnswer> specification);

    Task SaveResponses(ISpecification<ProjectApplicationRespondentAnswer> specification, List<ProjectApplicationRespondentAnswer> respondentAnswers);
}