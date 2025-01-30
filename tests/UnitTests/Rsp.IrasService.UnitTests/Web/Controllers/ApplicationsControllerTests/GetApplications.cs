namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetApplications : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetApplications()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetApplications_ShouldReturnApplications_WhenDataExists(List<ApplicationResponse> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetApplicationsQuery>(), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplications();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockResponse, result);
    }

    [Fact]
    public async Task GetApplications_ShouldReturnEmptyList_WhenNoDataExists()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetApplicationsQuery>(), default))
            .ReturnsAsync(new List<ApplicationResponse>());

        // Act
        var result = await _controller.GetApplications();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}