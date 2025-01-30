namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class CreateApplication : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public CreateApplication()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task CreateApplication_ShouldSendCommand_AndReturnCreatedApplication(ApplicationRequest request,
        ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<CreateApplicationCommand>(c => c.CreateApplicationRequest == request), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.CreateApplication(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockResponse, result);
        mockMediator.Verify(
            m => m.Send(It.Is<CreateApplicationCommand>(c => c.CreateApplicationRequest == request), default),
            Times.Once);
    }
}