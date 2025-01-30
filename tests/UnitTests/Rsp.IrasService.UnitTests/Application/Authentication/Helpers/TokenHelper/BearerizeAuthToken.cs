namespace Rsp.IrasService.UnitTests.Application.Authentication.Helpers.TokenHelper;

public class BearerizeAuthToken
{
    [Fact]
    public void BearerizeAuthToken_Should_ThrowException_When_AuthTokenIsEmpty()
    {
        // Arrange
        var helper = new IrasService.Application.Authentication.Helpers.TokenHelper();
        var emptyToken = new StringValues();

        // Act & Assert
        var exception = Assert.Throws<SecurityTokenException>(() => helper.BearerizeAuthToken(emptyToken));
        Assert.Equal("Empty authorization token", exception.Message);
    }

    [Fact]
    public void BearerizeAuthToken_Should_AddBearerPrefix_When_TokenDoesNotHaveBearerPrefix()
    {
        // Arrange
        var helper = new IrasService.Application.Authentication.Helpers.TokenHelper();
        var tokenWithoutBearer = new StringValues("some-token");

        // Act
        var result = helper.BearerizeAuthToken(tokenWithoutBearer);

        // Assert
        Assert.Equal("Bearer some-token", result);
    }

    [Fact]
    public void BearerizeAuthToken_Should_ReturnToken_When_TokenAlreadyHasBearerPrefix()
    {
        // Arrange
        var helper = new IrasService.Application.Authentication.Helpers.TokenHelper();
        var tokenWithBearer = new StringValues("Bearer some-token");

        // Act
        var result = helper.BearerizeAuthToken(tokenWithBearer);

        // Assert
        Assert.Equal("Bearer some-token", result);
    }
}