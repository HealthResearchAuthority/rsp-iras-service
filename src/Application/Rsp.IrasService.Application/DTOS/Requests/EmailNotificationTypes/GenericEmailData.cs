namespace Rsp.IrasService.Application.DTOS.Requests.EmailNotificationTypes;

public class GenericEmailData
{
    public IEnumerable<string> UserIds { get; set; } = [];
}