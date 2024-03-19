using Moq;
using Moq.AutoMock;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.WebApi.Controllers;
using Shouldly;

namespace Rsp.IrasService.UnitTests;

public class CategoriesControllerTests
{
    private readonly AutoMocker _mocker;
    private readonly CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _mocker = new AutoMocker();
        _controller = _mocker.CreateInstance<CategoriesController>();
    }

    [Fact]
    public async Task GetApplicationCategories_ReturnsCorrectList()
    {
        // Arrange
        var expectedCategories = new List<string> { "Category1", "Category2" };
        _mocker.GetMock<ICategoriesService>()
            .Setup(service => service.GetApplicationCategories())
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _controller.GetApplicationCategories();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(expectedCategories);
    }

    [Fact]
    public async Task AddApplicationCategory_ShouldAddNewCategory()
    {
        // Arrange
        var expectedCategories = new List<string> { "Category1", "Category2" };

        var category = "Category3";

        _mocker
            .GetMock<ICategoriesService>()
            .Setup(service => service.AddApplicationCategory(It.IsAny<string>()))
            .Callback<string>(expectedCategories.Add);

        // Act
        await _controller.AddApplicationCategory(category);

        // Assert
        expectedCategories.ShouldContain(category);
    }

    [Fact]
    public async Task GetProjectCategories_ReturnsCorrectList()
    {
        // Arrange
        var expectedCategories = new List<string> { "Project1", "Project2" };
        _mocker.GetMock<ICategoriesService>()
            .Setup(service => service.GetProjectCategories())
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _controller.GetProjectCategories();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(expectedCategories);
    }

    [Fact]
    public async Task AddProjectCategory_ShouldAddNewCategory()
    {
        // Arrange
        var expectedCategories = new List<string> { "Project1", "Project2" };

        var category = "Project3";

        _mocker
            .GetMock<ICategoriesService>()
            .Setup(service => service.AddProjectCategory(It.IsAny<string>()))
            .Callback<string>(expectedCategories.Add);

        // Act
        await _controller.AddProjectCategory(category);

        // Assert
        expectedCategories.ShouldContain(category);
    }
}