namespace Rsp.IrasService.UnitTests.Web.Controllers.ProbesControllerTests;

public class Liveness
{
    [Fact]
    public void Liveness_Should_ReturnOkResult()
    {
        // Arrange
        var controller = new ProbesController();

        // Act
        var result = controller.Liveness();

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}