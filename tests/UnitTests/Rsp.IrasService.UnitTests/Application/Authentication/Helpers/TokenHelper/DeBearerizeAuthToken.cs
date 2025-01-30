namespace Rsp.IrasService.UnitTests.Application.Authentication.Helpers.TokenHelper;

public class DeBearerizeAuthToken
{
    [Fact]
    public void DeBearerizeAuthToken_Should_ThrowException_When_AuthTokenIsEmpty()
    {
        // Arrange
        var helper = new IrasService.Application.Authentication.Helpers.TokenHelper();
        var emptyToken = new StringValues();

        // Act & Assert
        var exception = Assert.Throws<SecurityTokenException>(() => helper.DeBearerizeAuthToken(emptyToken));
        Assert.Equal("Empty authorization token", exception.Message);
    }

    [Fact]
    public void DeBearerizeAuthToken_Should_RemoveBearerPrefix_When_TokenHasBearerPrefix()
    {
        // Arrange
        var helper = new IrasService.Application.Authentication.Helpers.TokenHelper();
        var tokenWithBearer = new StringValues("Bearer some-token");

        // Act
        var result = helper.DeBearerizeAuthToken(tokenWithBearer);

        // Assert
        Assert.Equal("some-token", result);
    }
}