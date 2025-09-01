using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetAllModifications : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public GetAllModifications()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetAllModifications_ShouldReturnOk_WhenModificationsExist
    (
        ModificationSearchRequest searchQuery,
        ModificationResponse mockResponse,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetModificationsQuery>(q =>
                q.SearchQuery.Equals(searchQuery) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetAllModifications(searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task GetAllModifications_ShouldReturnBadRequest_WhenPageParamsAreInvalid(int pageNumber, int pageSize)
    {
        // Arrange
        var searchQuery = new ModificationSearchRequest();
        var sortField = "CreatedAt";
        var sortDirection = "asc";

        // Act
        var result = await _controller.GetAllModifications(searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        var badRequestResult = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.StatusCode.ShouldBe(400);
        badRequestResult.Value.ShouldBeOfType<string>();
    }
}