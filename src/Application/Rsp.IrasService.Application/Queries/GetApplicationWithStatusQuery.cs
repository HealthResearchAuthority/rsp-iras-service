namespace Rsp.IrasService.Application.Queries;

public class GetApplicationWithStatusQuery(int id) : GetApplicationQuery(id)
{
    public string ApplicationStatus { get; set; } = null!;
}