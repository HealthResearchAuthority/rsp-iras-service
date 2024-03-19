using Moq.AutoMock;
using Rsp.IrasService.Services;
using Shouldly;

namespace Rsp.IrasService.UnitTests;

public class CategoriesServiceTests
{
    private readonly AutoMocker _mocker;
    private readonly CategoriesService _service;

    public CategoriesServiceTests()
    {
        _mocker = new AutoMocker();
        _service = _mocker.CreateInstance<CategoriesService>();
    }

    [Fact]
    public async Task AddApplicationCategory_NotAlreadyExists_AddsCategory()
    {
        // Arrange
        var categoryName = "New Category";

        // Act
        await _service.AddApplicationCategory(categoryName);

        // Assert
        (await _service.GetApplicationCategories()).ShouldContain(categoryName);
    }

    [Fact]
    public async Task AddApplicationCategory_AlreadyExists_NoDuplicateCategory()
    {
        // Arrange
        var categoryName = "IRAS Form";

        // Act
        await _service.AddApplicationCategory(categoryName);

        // Assert
        (await _service.GetApplicationCategories()).Count().ShouldBe(4); // No duplicates added
    }

    [Fact]
    public async Task AddProjectCategory_NotAlreadyExists_AddsCategory()
    {
        // Arrange
        var projectName = "New Project";

        // Act
        await _service.AddProjectCategory(projectName);

        // Assert
        (await _service.GetProjectCategories()).ShouldContain(projectName);
    }

    [Fact]
    public async Task AddProjectCategory_AlreadyExists_NoDuplicateCategory()
    {
        // Arrange
        var projectName = "Ionising Radiation";

        // Act
        await _service.AddProjectCategory(projectName);

        // Assert
        (await _service.GetProjectCategories()).Count().ShouldBe(7); // No duplicates added
    }

    [Fact]
    public async Task GetApplicationCategories_ReturnsCorrectList()
    {
        // Act
        var categories = await _service.GetApplicationCategories();

        // Assert
        categories.ShouldBeOfType<List<string>>();
    }

    [Fact]
    public async Task GetProjectCategories_ReturnsCorrectList()
    {
        // Act
        var categories = await _service.GetProjectCategories();

        // Assert
        categories.ShouldBeOfType<List<string>>();
    }
}