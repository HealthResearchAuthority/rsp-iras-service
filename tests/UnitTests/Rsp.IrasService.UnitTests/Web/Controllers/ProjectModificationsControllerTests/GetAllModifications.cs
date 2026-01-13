using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

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
        ModificationSearchResponse mockResponse,
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

    [Fact]
    public async Task GetAllModifications_PassesAllowedStatusesFromSearchQuery_ToMediator()
    {
        var mediator = Mocker.GetMock<IMediator>();

        var searchQuery = new ModificationSearchRequest
        {
            AllowedStatuses = new List<string> { "WithSponsor", "Approved" }
        };

        var expectedResponse = new ModificationSearchResponse
        {
            Modifications = new List<ModificationDto>(),
            TotalCount = 0
        };

        mediator
            .Setup(m => m.Send(
                It.Is<GetModificationsQuery>(q => q.SearchQuery == searchQuery && q.AllowedStatuses.SequenceEqual(searchQuery.AllowedStatuses)),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        var result = await _controller.GetAllModifications(searchQuery, 1, 10, "f", "d");

        result.Value.ShouldBe(expectedResponse);

        mediator.Verify(m => m.Send(
            It.Is<GetModificationsQuery>(q => q.SearchQuery == searchQuery && q.AllowedStatuses.SequenceEqual(searchQuery.AllowedStatuses)),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllModifications_DoesNotSetAllowedStatuses_WhenSearchQueryHasNone()
    {
        var mediator = Mocker.GetMock<IMediator>();

        var searchQuery = new ModificationSearchRequest();

        var expectedResponse = new ModificationSearchResponse
        {
            Modifications = new List<ModificationDto>(),
            TotalCount = 0
        };

        mediator
            .Setup(m => m.Send(
                It.Is<GetModificationsQuery>(q => q.SearchQuery == searchQuery && (q.AllowedStatuses == null || !q.AllowedStatuses.Any())),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        var result = await _controller.GetAllModifications(searchQuery, 1, 10, "f", "d");

        result.Value.ShouldBe(expectedResponse);

        mediator.Verify(m => m.Send(
            It.Is<GetModificationsQuery>(q => q.SearchQuery == searchQuery && (q.AllowedStatuses == null || !q.AllowedStatuses.Any())),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}