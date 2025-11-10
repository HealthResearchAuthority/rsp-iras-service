using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetPaginatedApplications : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetPaginatedApplications()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetPaginatedApplications_ShouldReturnApplications_WhenDataExists(
        PaginatedResponse<CompleteProjectRecordResponse> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetPaginatedProjectRecordsQuery>(), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetPaginatedApplications(
            It.IsAny<ProjectRecordSearchRequest>(),
            1,
            1);

        // Assert
        result.ShouldNotBeNull();
        var objectResult = result.Result.ShouldBeOfType<OkObjectResult>();
        objectResult.Value.ShouldBeOfType<PaginatedResponse<CompleteProjectRecordResponse>>();
    }

    [Fact]
    public async Task GetPaginatedApplications_ShouldReturnBadRequest_WhenNoPagination()
    {
        // Arrange

        // Act
        var result = await _controller.GetPaginatedApplications(It.IsAny<ProjectRecordSearchRequest>(), -1);

        // Assert
        result.Result.ShouldBeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GetPaginatedApplications_ShouldReturnBadRequest_WhenPageSizeNegative()
    {
        // Arrange

        // Act
        var result = await _controller.GetPaginatedApplications(It.IsAny<ProjectRecordSearchRequest>(), 1, -1);

        // Assert
        result.Result.ShouldBeOfType<BadRequestObjectResult>();
    }
}