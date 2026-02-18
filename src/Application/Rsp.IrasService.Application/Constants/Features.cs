namespace Rsp.Service.Application.Constants;

/// <summary>
/// Defines constants for feature names.
/// </summary>
public static class Features
{
    // Name of the Intercepted Logging feature.
    public const string InterceptedLogging = "Logging.InterceptedLogging";

    // Uses Gov UK One Login if enabled
    public const string OneLogin = "Auth.UseOneLogin";

    // For document management, allows users to supersede documents if enabled
    public const string SupersedingDocuments = "Modifications.SupersedingDocuments";
}