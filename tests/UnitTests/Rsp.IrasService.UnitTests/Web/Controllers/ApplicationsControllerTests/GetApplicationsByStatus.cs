namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetApplicationsByStatus : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetApplicationsByStatus()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetApplicationsByStatus_ShouldReturnApplications_WhenDataExists(string status,
        List<ApplicationResponse> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationsWithStatusQuery>(q => q.ApplicationStatus == status), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplicationsByStatus(status);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockResponse, result);
    }

    [Theory]
    [AutoData]
    public async Task GetApplicationsByStatus_ShouldReturnEmptyList_WhenNoDataExists(string status)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationsWithStatusQuery>(q => q.ApplicationStatus == status), default))
            .ReturnsAsync(new List<ApplicationResponse>());

        // Act
        var result = await _controller.GetApplicationsByStatus(status);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}