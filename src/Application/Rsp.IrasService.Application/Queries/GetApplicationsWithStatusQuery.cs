namespace Rsp.IrasService.Application.Queries;

public class GetApplicationsWithStatusQuery : GetApplicationsQuery
{
    public string ApplicationStatus { get; set; } = null!;
}