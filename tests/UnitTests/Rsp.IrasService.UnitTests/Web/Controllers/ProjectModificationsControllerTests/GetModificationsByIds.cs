using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetModificationsByIds : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task GetModificationsById_ShouldReturnOk_WhenModificationsExist
    (
        List<string> ids,
        ModificationSearchResponse mockResponse
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send
            (
                It.Is<GetModificationsByIdsQuery>(q => q.Ids == ids), default)
            )
            .ReturnsAsync(mockResponse);
        // Act
        var result = await Sut.GetModificationsByIds(ids);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }

    [Fact]
    public async Task GetModificationsByID_ShouldReturnBadRequest_WhenIdsDoNotExist()
    {
        // Arrange
        List<string> ids = [];
        // Act
        var result = await Sut.GetModificationsByIds(ids);
        // Assert
        result.Result.ShouldBeOfType<BadRequestObjectResult>();
    }
}