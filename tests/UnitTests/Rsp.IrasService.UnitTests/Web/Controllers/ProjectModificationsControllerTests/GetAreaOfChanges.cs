using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetAreaOfChanges : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public GetAreaOfChanges()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetAreaOfChanges_ShouldReturnAreas_WhenDataExists(List<ModificationAreaOfChangeDto> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetModificationAreaOfChangeQuery>(), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetAreaOfChanges();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);
    }

    [Fact]
    public async Task GetAreaOfChanges_ShouldReturnEmptyList_WhenNoDataExists()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetModificationAreaOfChangeQuery>(), default))
            .ReturnsAsync(new List<ModificationAreaOfChangeDto>());

        // Act
        var result = await _controller.GetAreaOfChanges();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}