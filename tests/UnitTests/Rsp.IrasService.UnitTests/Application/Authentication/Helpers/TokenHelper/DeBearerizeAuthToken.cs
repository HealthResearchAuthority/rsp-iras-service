using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

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
        exception.Message.ShouldBe("Empty authorization token");
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
        result.ShouldBe("some-token");
    }
}