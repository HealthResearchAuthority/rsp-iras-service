namespace Rsp.IrasService.UnitTests.Web.Controllers.ProbesControllerTests;

public class Startup
{
    [Fact]
    public void Startup_Should_ReturnOkResult()
    {
        // Arrange
        var controller = new ProbesController();

        // Act
        var result = controller.Startup();

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}