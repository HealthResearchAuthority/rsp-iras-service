namespace Rsp.IrasService.Application.CQRS.Queries;

public abstract class BaseQuery
{
    public List<string> AllowedStatuses { get; set; } = [];
}