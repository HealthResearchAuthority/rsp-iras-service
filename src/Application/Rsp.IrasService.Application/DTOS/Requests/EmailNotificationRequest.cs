namespace Rsp.IrasService.Domain.Entities.Notifications
{    
    public class EmailNotificationRequest : INotificationMessage
    {
        public int EventType { get; set; }
        public string EventName { get; set; } = null!;
        public string EmailTemplateId { get; set; } = null!;
        /// <summary>
        /// List of recipient email addresses 
        /// </summary>
        public IList<string> RecipientAdresses { get; set; } = null!;
        /// <summary>
        /// Personalisation data for any placeholder fields in the email template 
        /// </summary>
        public IDictionary<string, dynamic> Data { get; set; } = new Dictionary<string, dynamic>();
    }
}
