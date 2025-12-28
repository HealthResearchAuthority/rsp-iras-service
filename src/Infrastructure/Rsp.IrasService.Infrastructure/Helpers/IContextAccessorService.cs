namespace Rsp.IrasService.Infrastructure.Helpers;

public interface IContextAccessorService
{
    public string GetUserEmail();

    public string GetUserId();
}