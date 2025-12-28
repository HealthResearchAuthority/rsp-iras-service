namespace Rsp.Service.Infrastructure.Helpers;

public interface IContextAccessorService
{
    public string GetUserEmail();

    public string GetUserId();
}