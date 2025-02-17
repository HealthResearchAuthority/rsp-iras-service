using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsp.IrasService.Application.DTOS.Requests
{
    public class EmailNotificationQueueRequest
    {
        public IEnumerable<EmailNotificationQueueMessage> NotificationMessages { get; set; }
    }
}
