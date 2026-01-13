using Microsoft.AspNetCore.Mvc;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProbesControllerTests;

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
        var okResult = result.ShouldBeOfType<OkResult>();
        okResult.StatusCode.ShouldBe(200);
    }
}