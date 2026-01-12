using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetProjectClosureSpecification : Specification<ProjectClosure>
{
    public GetProjectClosureSpecification(string projectRecordId)
    {
        Query.Where(entity => entity.ProjectRecordId == projectRecordId);
    }
}