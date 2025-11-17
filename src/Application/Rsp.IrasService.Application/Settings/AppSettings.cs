namespace Rsp.IrasService.Application.Settings;

public class AppSettings
{
    /// <summary>
    /// Label to use when reading App Configuration from AzureAppConfiguration
    /// </summary>
    public const string ServiceLabel = "applicationservice";

    /// <summary>
    /// Gets or sets authentication settings
    /// </summary>
    public AuthSettings AuthSettings { get; set; } = null!;

    /// <summary>
    /// Gets or sets Azure App Configuration settings
    /// </summary>
    public AzureAppConfigurations AzureAppConfiguration { get; set; } = null!;

    /// <summary>
    /// Gets or sets Azure Service Bus settings
    /// </summary>
    public AzureServiceBusSettings AzureServiceBusSettings { get; set; } = null!;

    /// <summary>
    /// Gets or sets OneLogin settings
    /// </summary>
    public OneLoginConfiguration OneLogin { get; set; } = null!;

    public Dictionary<string, string> QuestionIds { get; set; } = null!;

    public MicrosoftEntra MicrosoftEntra { get; set; } = null!;
}