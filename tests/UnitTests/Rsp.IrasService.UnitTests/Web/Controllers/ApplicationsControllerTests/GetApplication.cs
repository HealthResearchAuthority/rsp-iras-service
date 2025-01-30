namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetApplication : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetApplication()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_ShouldReturnOk_WhenApplicationExists(string applicationId,
        ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationQuery>(q => q.ApplicationId == applicationId), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplication(applicationId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(mockResponse, okResult.Value);
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_ShouldReturnNotFound_WhenApplicationDoesNotExist(string applicationId)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationQuery>(q => q.ApplicationId == applicationId), default))
            .ReturnsAsync((ApplicationResponse)null);

        // Act
        var result = await _controller.GetApplication(applicationId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_WithStatus_ShouldReturnOk_WhenApplicationExists(string applicationId,
        string status, ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetApplicationWithStatusQuery>(q =>
                    q.ApplicationId == applicationId && q.ApplicationStatus == status), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplication(applicationId, status);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(mockResponse, okResult.Value);
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_WithStatus_ShouldReturnNotFound_WhenApplicationDoesNotExist(string applicationId,
        string status)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetApplicationWithStatusQuery>(q =>
                    q.ApplicationId == applicationId && q.ApplicationStatus == status), default))
            .ReturnsAsync((ApplicationResponse)null);

        // Act
        var result = await _controller.GetApplication(applicationId, status);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}