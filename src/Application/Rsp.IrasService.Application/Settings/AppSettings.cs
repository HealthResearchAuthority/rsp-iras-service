﻿namespace Rsp.IrasService.Application.Settings;

public class AppSettings
{
    /// <summary>
    /// Gets or sets authentication settings
    /// </summary>
    public AuthSettings AuthSettings { get; set; } = null!;

    public AzureAppConfigurations AzureAppConfiguration { get; set; } = null!;
}