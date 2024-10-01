namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetApplicationWithStatusQuery(string applicationId) : GetApplicationQuery(applicationId)
{
    public string ApplicationStatus { get; set; } = null!;
}