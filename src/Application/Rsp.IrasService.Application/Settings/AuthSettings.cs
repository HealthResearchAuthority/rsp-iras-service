namespace Rsp.IrasService.Application.Settings;

public class AuthSettings
{
    /// <summary>
    /// The value for valid issuers
    /// </summary>
    public List<string> Issuers { get; set; } = null!;

    /// <summary>
    /// The client identifier.
    /// </summary>
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// The client identifier.
    /// </summary>
    public string JwksUri { get; set; } = null!;
}