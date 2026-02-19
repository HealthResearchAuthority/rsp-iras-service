using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Extensions;

namespace Rsp.IrasService.UnitTests.Application.Extensions;

public class ModificationStatusExtensionsTests
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("   ", "")]
    public void ToUiRevisionStatus_ReturnsEmpty_WhenNullOrWhitespace(string? input, string expected)
    {
        // Act
        var result = input.ToUiRevisionStatus();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToUiRevisionStatus_Maps_RequestRevisions_To_InDraft()
    {
        // Arrange
        var input = ModificationStatus.RequestRevisions;

        // Act
        var result = input.ToUiRevisionStatus();

        // Assert
        Assert.Equal(ModificationStatus.InDraft, result);
    }

    [Fact]
    public void ToUiRevisionStatus_Maps_ReviseAndAuthorise_To_WithSponsor()
    {
        // Arrange
        var input = ModificationStatus.ReviseAndAuthorise;

        // Act
        var result = input.ToUiRevisionStatus();

        // Assert
        Assert.Equal(ModificationStatus.WithSponsor, result);
    }

    [Fact]
    public void ToUiRevisionStatus_ReturnsOriginal_ForUnmappedValues()
    {
        // Arrange
        var input = "SomeStatus";

        // Act
        var result = input.ToUiRevisionStatus();

        // Assert
        Assert.Equal("SomeStatus", result);
    }
}