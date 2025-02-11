using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications
{
    public class GetEmailTemplateSpecification : Specification<EmailTemplate>
    {

        public GetEmailTemplateSpecification(string eventTypeId)
        {
            Query.AsNoTracking()
                .Where(x => x.EventTypeId == eventTypeId);
        }
    }
}
