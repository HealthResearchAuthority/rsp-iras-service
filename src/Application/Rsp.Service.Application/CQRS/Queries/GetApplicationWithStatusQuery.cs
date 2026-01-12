namespace Rsp.Service.Application.CQRS.Queries;

public class GetApplicationWithStatusQuery(string applicationId) : GetApplicationQuery(applicationId)
{
    public string ApplicationStatus { get; set; } = null!;
}