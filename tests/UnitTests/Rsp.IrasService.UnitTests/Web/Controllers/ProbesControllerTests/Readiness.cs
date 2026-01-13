using Microsoft.AspNetCore.Mvc;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProbesControllerTests;

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
        var okResult = result.ShouldBeOfType<OkResult>();
        okResult.StatusCode.ShouldBe(200);
    }
}