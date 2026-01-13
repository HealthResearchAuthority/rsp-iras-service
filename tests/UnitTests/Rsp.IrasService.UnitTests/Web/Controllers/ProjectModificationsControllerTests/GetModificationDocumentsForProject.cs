using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetModificationDocumentsForProject : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public GetModificationDocumentsForProject()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetModificationDocumentsForProject_ShouldReturnOk_WhenDocumentsExist
    (
        string projectRecordId,
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
                q.ProjectRecordId.Equals(projectRecordId) &&
                q.SearchQuery.Equals(searchQuery) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetModificationDocumentsForProject(projectRecordId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task GetModificationDocumentsForProject_ShouldReturnBadRequest_WhenPageParamsAreInvalid(int pageNumber, int pageSize)
    {
        // Arrange
        var projectRecordId = "test-project";
        var searchQuery = new ProjectOverviewDocumentSearchRequest();
        var sortField = "CreatedAt";
        var sortDirection = "asc";

        // Act
        var result = await _controller.GetModificationDocumentsForProject(projectRecordId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        var badRequestResult = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.StatusCode.ShouldBe(400);
        badRequestResult.Value.ShouldBeOfType<string>();
    }
}