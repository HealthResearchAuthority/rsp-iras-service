namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetApplicationsWithRespondentQuery : GetApplicationsQuery
{
    public string RespondentId { get; set; } = null!;
}