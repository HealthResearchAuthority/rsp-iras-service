using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class ApplicationsControllerTests : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public ApplicationsControllerTests()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task UpdateApplication_ShouldSendCommand_AndReturnUpdatedApplication(ApplicationRequest request,
        ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<UpdateApplicationCommand>(c => c.UpdateApplicationRequest == request), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.UpdateApplication(request);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);
        mockMediator.Verify(
            m => m.Send(It.Is<UpdateApplicationCommand>(c => c.UpdateApplicationRequest == request), default),
            Times.Once);
    }
}