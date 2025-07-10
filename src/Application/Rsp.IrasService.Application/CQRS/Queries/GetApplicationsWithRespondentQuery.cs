namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetApplicationsWithRespondentQuery : GetApplicationsQuery
{
    public string RespondentId { get; set; } = null!;
    public string? SearchQuery { get; set; } = null;
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}