using Rsp.IrasService.Services;
using Shouldly;

namespace Rsp.IrasService.UnitTests.CategoriesServiceTests;

public class AddApplicationCategory : TestServiceBase<CategoriesService>
{
    [Fact]
    public async Task NotAlreadyExists_AddsCategory()
    {
        // Arrange
        const string categoryName = "New Category";

        // Act
        await Sut.AddApplicationCategory(categoryName);

        // Assert
        (await Sut.GetApplicationCategories()).ShouldContain(categoryName);
    }

    [Fact]
    public async Task AlreadyExists_NoDuplicateCategory()
    {
        // Arrange
        const string categoryName = "IRAS Form";

        // Act
        await Sut.AddApplicationCategory(categoryName);

        // Assert
        (await Sut.GetApplicationCategories()).Count().ShouldBe(4); // No duplicates added
    }
}