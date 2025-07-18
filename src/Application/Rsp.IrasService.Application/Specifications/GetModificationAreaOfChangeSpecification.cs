using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetModificationAreaOfChangeSpecification : Specification<ModificationAreaOfChange>
{
    public GetModificationAreaOfChangeSpecification()
    {
        Query
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.ModificationSpecificAreaOfChanges);
    }
}