using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Rsp.Service.UnitTests.Application.Authentication.Helpers.TokenHelper;

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
        exception.Message.ShouldBe("Empty authorization token");
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
        result.ShouldBe("Bearer some-token");
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
        result.ShouldBe("Bearer some-token");
    }
}