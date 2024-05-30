using Rsp.IrasService.Services;
using Shouldly;

namespace Rsp.IrasService.UnitTests.CategoriesServiceTests;

public class GetCategories : TestServiceBase<CategoriesService>
{
    [Fact]
    public async Task ReturnsApplicationCategories()
    {
        // Act
        var categories = await Sut.GetApplicationCategories();

        // Assert
        categories.ShouldBeOfType<List<string>>();
    }

    [Fact]
    public async Task ReturnsProjectCategories()
    {
        // Act
        var categories = await Sut.GetProjectCategories();

        // Assert
        categories.ShouldBeOfType<List<string>>();
    }
}