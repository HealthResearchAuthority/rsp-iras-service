using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.WebApi.Controllers;

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
        var okResult = result.ShouldBeOfType<OkResult>();
        okResult.StatusCode.ShouldBe(200);
    }
}