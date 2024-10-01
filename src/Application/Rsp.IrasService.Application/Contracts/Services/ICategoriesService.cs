namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to get/add the application and project categories
/// </summary>
public interface ICategoriesService
{
    /// <summary>
    /// Returns the list of research application categories
    /// </summary>
    Task<IEnumerable<string>> GetApplicationCategories();

    /// <summary>
    /// Returns the list of project categories
    /// </summary>
    Task<IEnumerable<string>> GetProjectCategories();

    /// <summary>
    /// Adds a new application category to the list
    /// </summary>
    /// <param name="categoryName">Name of the category</param>
    Task AddApplicationCategory(string categoryName);

    /// <summary>
    /// Adds a new project to the list
    /// </summary>
    /// <param name="projectName">Name of the project</param>
    Task AddProjectCategory(string projectName);
}