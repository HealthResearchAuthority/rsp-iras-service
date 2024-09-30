namespace Rsp.IrasService.Application.Queries;

public class GetApplicationWithStatusQuery(string id) : GetApplicationQuery(id)
{
    public string ApplicationStatus { get; set; } = null!;
}