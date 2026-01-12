using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Rsp.Service.Infrastructure.Middlewares;

/// <summary>
/// This middleware will validate if the user has access to the requested resource
/// by checking their roles and see if they have access to the project, modification or document.
/// </summary>
[ExcludeFromCodeCoverage]
public static class AccessValidationExtensions
{
    /// <summary>
    /// Adds <see cref="ProjectRecordAccessValidationMiddleware"/> middleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseProjectRecordAccessValidation(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        return app.UseMiddleware<ProjectRecordAccessValidationMiddleware>();
    }

    /// <summary>
    /// Adds <see cref="ModificationAccessValidationMiddleware"/> middleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseModificationAccessValidation(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        return app.UseMiddleware<ModificationAccessValidationMiddleware>();
    }

    /// <summary>
    /// Adds <see cref="DocumentAccessValidationMiddleware"/> middleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseDocumentAccessValidation(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        return app.UseMiddleware<DocumentAccessValidationMiddleware>();
    }
}