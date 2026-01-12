using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetEmailTemplateSpecification : Specification<EmailTemplate>
{
    public GetEmailTemplateSpecification(string eventTypeId)
    {
        Query.AsNoTracking()
            .Where(x => x.EventTypeId == eventTypeId)
            .Include(x => x.EventType);
    }
}