using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetProjectClosureSpecification : Specification<ProjectClosure>
{
    public GetProjectClosureSpecification(string projectRecordId)
    {
        Query.Where(entity => entity.ProjectRecordId == projectRecordId);
    }
}