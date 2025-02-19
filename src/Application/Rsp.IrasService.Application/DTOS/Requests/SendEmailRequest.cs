﻿namespace Rsp.IrasService.Application.DTOS.Requests;

public class TriggerSendEmailRequest
{
    public string EventTypeId { get; set; } = null!;
    public IEnumerable<string> EmailRecipients { get; set; } = Enumerable.Empty<string>();
    public IDictionary<string, dynamic> Data { get; set; } = new Dictionary<string, dynamic>();
}