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
    public async Task GetDocumentsForModification_ShouldReturnResponse_WhenDocumentsExist(
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
        var mediatorMock = Mocker.GetMock<IMediator>();

        mediatorMock
            .Setup(m => m.Send(It.Is<GetDocumentsForModificationQuery>(q =>
                q.ModificationId == modificationId &&
                q.SearchQuery == searchQuery &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize &&
                q.SortField == sortField &&
                q.SortDirection == sortDirection
            ), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetDocumentsForModification(
            modificationId,
            searchQuery,
            pageNumber,
            pageSize,
            sortField,
            sortDirection
        );

        // Assert
        result.Result.ShouldBeNull();             // Should not return BadRequest()
        result.Value.ShouldBe(mockResponse);      // Controller should return mediator response

        mediatorMock.Verify(m => m.Send(It.IsAny<GetDocumentsForModificationQuery>(), default), Times.Once);
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