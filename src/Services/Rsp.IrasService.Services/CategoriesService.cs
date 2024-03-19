using Rsp.IrasService.Application.Contracts;

namespace Rsp.IrasService.Services;

public class CategoriesService : ICategoriesService
{
    private static List<string> _applicationCategories = ["IRAS Form", "Confidentiality Advisory Group (CAG)", "HM Prison and Probation Services (HMPPS)"];
    private static List<string> _projectCategories = ["Ionising Radiation", "Clinical Investigation", "Basic Science Study", "Research Tissue Bank", "Research database", "Other study"];

    /// <summary>
    /// Adds a new application category to the list
    /// </summary>
    /// <param name="categoryName">Name of the category</param>
    public Task AddApplicationCategory(string categoryName)
    {
        if (!_applicationCategories.Contains(categoryName))
        {
            _applicationCategories.Add(categoryName);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Adds a new project to the list
    /// </summary>
    /// <param name="projectName">Name of the project</param>
    public Task AddProjectCategory(string projectName)
    {
        if (!_projectCategories.Contains(projectName, StringComparer.OrdinalIgnoreCase))
        {
            _projectCategories.Add(projectName);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns the list of research application categories
    /// </summary>
    public Task<IEnumerable<string>> GetApplicationCategories()
    {
        return Task.FromResult(_applicationCategories.AsEnumerable());
    }

    /// <summary>
    /// Returns the list of project categories
    /// </summary>
    public Task<IEnumerable<string>> GetProjectCategories()
    {
        return Task.FromResult(_projectCategories.AsEnumerable());
    }
}