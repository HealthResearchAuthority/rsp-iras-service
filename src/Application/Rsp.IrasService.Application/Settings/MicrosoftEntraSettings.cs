namespace Rsp.IrasService.Application.Settings;

public class MicrosoftEntraSettings
{
    /// <summary>
    /// Authrity URL for the Microsoft Entra tenant
    /// </summary>
    public string Authority { get; set; } = null!;

    /// <summary>
    /// The API Client ID for the IRAS Service from Microsoft Entra
    /// </summary>
    public string IrasServiceAPIID { get; set; } = null!;
}