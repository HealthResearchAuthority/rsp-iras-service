namespace Rsp.IrasService.UnitTests.Web.Controllers.ProbesControllerTests;

public class Readiness
{
    [Fact]
    public void Readiness_Should_ReturnOkResult()
    {
        // Arrange
        var controller = new ProbesController();

        // Act
        var result = controller.Readiness();

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}