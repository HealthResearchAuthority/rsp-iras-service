using Rsp.IrasService.Services;
using Shouldly;

namespace Rsp.IrasService.UnitTests.CategoriesServiceTests;

public class AddProjectCategory : TestServiceBase<CategoriesService>
{
    [Fact]
    public async Task NotAlreadyExists_AddsCategory()
    {
        // Arrange
        const string projectName = "New Project";

        // Act
        await Sut.AddProjectCategory(projectName);

        // Assert
        (await Sut.GetProjectCategories()).ShouldContain(projectName);
    }

    [Fact]
    public async Task AlreadyExists_NoDuplicateCategory()
    {
        // Arrange
        const string projectName = "Ionising Radiation";

        // Act
        await Sut.AddProjectCategory(projectName);

        // Assert
        (await Sut.GetProjectCategories()).Count().ShouldBe(6); // No duplicates added
    }
}