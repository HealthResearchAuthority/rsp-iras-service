using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetDocumentsForModification : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public GetDocumentsForModification()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetDocumentsForModification_ShouldReturnOk_WhenDocumentsExist
    (
        Guid modificationId,
        ProjectOverviewDocumentSearchRequest searchQuery,
        ProjectOverviewDocumentResponse mockResponse,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetDocumentsForProjectOverviewQuery>(q =>
                q.ProjectRecordId.Equals(modificationId) &&
                q.SearchQuery.Equals(searchQuery) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetDocumentsForModification(It.IsAny<Guid>(), It.IsAny<ProjectOverviewDocumentSearchRequest>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>());

        // Assert
        result.Value.ShouldBe(null);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task GetDocumentsForModification_ShouldReturnBadRequest_WhenPageParamsAreInvalid(int pageNumber, int pageSize)
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var searchQuery = new ProjectOverviewDocumentSearchRequest();
        var sortField = "CreatedAt";
        var sortDirection = "asc";

        // Act
        var result = await _controller.GetDocumentsForModification(modificationId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        var badRequestResult = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.StatusCode.ShouldBe(400);
        badRequestResult.Value.ShouldBeOfType<string>();
    }
}