using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetEmailTemplateSpecification : Specification<EmailTemplate>
{
    public GetEmailTemplateSpecification(string eventTypeId)
    {
        Query.AsNoTracking()
            .Where(x => x.EventTypeId == eventTypeId)
            .Include(x => x.EventType);
    }
}