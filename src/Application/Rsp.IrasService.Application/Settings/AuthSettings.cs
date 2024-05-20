﻿using System.Diagnostics.CodeAnalysis;

namespace Rsp.IrasService.Application.Settings;

[ExcludeFromCodeCoverage]
public class AuthSettings
{
    /// <summary>
    /// The value for token issuer / authority
    /// </summary>
    public string Authority { get; set; } = null!;

    /// <summary>
    /// The client identifier.
    /// </summary>
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// The client identifier.
    /// </summary>
    public string JwksUri { get; set; } = null!;
}