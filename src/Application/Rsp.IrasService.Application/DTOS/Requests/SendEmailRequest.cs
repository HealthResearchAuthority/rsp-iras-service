using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsp.IrasService.Application.DTOS.Requests
{
    public class SendEmailRequest
    {
        public string EventTypeId { get; set; } = null!;
        public IEnumerable<string> EmailRecipients { get; set; } = Enumerable.Empty<string>();
        public IDictionary<string, dynamic> Data { get; set; } = new Dictionary<string, dynamic>();
    }
}
