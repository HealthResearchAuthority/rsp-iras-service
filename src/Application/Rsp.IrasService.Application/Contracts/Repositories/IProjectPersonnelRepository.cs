using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IProjectPersonnelRepository
{
    Task<IEnumerable<ProjectRecordAnswer>> GetResponses(ISpecification<ProjectRecordAnswer> specification);

    Task SaveResponses(ISpecification<ProjectRecordAnswer> specification, List<ProjectRecordAnswer> respondentAnswers);
}