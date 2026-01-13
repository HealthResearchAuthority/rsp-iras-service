using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

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
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);
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
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}