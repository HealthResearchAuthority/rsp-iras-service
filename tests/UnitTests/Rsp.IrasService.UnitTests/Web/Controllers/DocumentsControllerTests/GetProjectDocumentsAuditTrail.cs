using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.DocumentsControllerTests;

public class GetProjectDocumentsAuditTrail : TestServiceBase<DocumentsController>
{
    [Theory]
    [AutoData]
    public async Task GetProjectDocumentsAuditTrail_ShouldReturnOk_WhenDocumentsExist
    (
        string projectRecordId,
        ProjectDocumentsAuditTrailResponse mockResponse,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetProjectDocumentsAuditTrailQuery>(q =>
                q.ProjectRecordId.Equals(projectRecordId) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.GetProjectDocumentsAuditTrail(projectRecordId, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task GetProjectDocumentsAuditTrail_ShouldReturnBadRequest_WhenPageParamsAreInvalid(int pageNumber, int pageSize)
    {
        // Arrange
        var projectRecordId = "test-project";
        var searchQuery = new ProjectOverviewDocumentSearchRequest();
        var sortField = "CreatedAt";
        var sortDirection = "asc";

        // Act
        var result = await Sut.GetProjectDocumentsAuditTrail(projectRecordId, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        var badRequestResult = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.StatusCode.ShouldBe(400);
        badRequestResult.Value.ShouldBeOfType<string>();
    }
}