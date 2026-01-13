using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetPaginatedApplicationsByRespondent : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetPaginatedApplicationsByRespondent()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetPaginatedApplicationsByRespondent_ShouldReturnApplications_WhenDataExists(string respondentId,
        PaginatedResponse<ApplicationResponse> mockResponse)
    {
        // Arrange
        var searchQuery = new ApplicationSearchRequest();
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup
            (
                m => m.Send
                (
                    It.Is<GetPaginatedApplicationsWithRespondentQuery>
                    (
                        q =>
                            q.RespondentId == respondentId &&
                            q.SearchQuery == searchQuery &&
                            q.PageIndex == 1 &&
                            q.PageSize == 5
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetPaginatedApplicationsByRespondent(searchQuery, respondentId, 1, 5);

        // Assert
        var ok = result.Result.ShouldBeOfType<OkObjectResult>();
        ok.Value.ShouldNotBeNull();
        ok.Value.ShouldBeEquivalentTo(mockResponse);
    }

    [Theory]
    [AutoData]
    public async Task GetPaginatedApplicationsByRespondent_ShouldReturnEmptyList_WhenNoDataExists(string respondentId)
    {
        // Arrange
        var searchQuery = new ApplicationSearchRequest();
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup
            (
                m => m.Send
                (
                    It.Is<GetPaginatedApplicationsWithRespondentQuery>
                        (
                            q =>
                                q.RespondentId == respondentId &&
                                q.SearchQuery == searchQuery &&
                                q.PageIndex == 1 &&
                                q.PageSize == 5
                        ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new PaginatedResponse<ApplicationResponse>
            {
                Items = new List<ApplicationResponse>(),
                TotalCount = 0
            });

        // Act
        var result = await _controller.GetPaginatedApplicationsByRespondent(searchQuery, respondentId, 1, 5);

        // Assert
        var ok = result.Result.ShouldBeOfType<OkObjectResult>();
        var paginatedResponse = ok.Value as PaginatedResponse<ApplicationResponse>;
        paginatedResponse.ShouldNotBeNull();
        paginatedResponse.Items.ShouldBeEmpty();
        paginatedResponse.TotalCount.ShouldBe(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetPaginatedApplicationsByRespondent_ShouldReturnBadRequest_WhenPageIndexInvalid(int pageIndex)
    {
        var searchQuery = new ApplicationSearchRequest();
        var respondentId = "abc";
        var result = await _controller.GetPaginatedApplicationsByRespondent(searchQuery, respondentId, pageIndex: pageIndex);

        var badRequest = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequest.Value.ShouldBe("pageIndex must be greater than 0 if specified.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public async Task GetPaginatedApplicationsByRespondent_ShouldReturnBadRequest_WhenPageSizeInvalid(int pageSize)
    {
        var searchQuery = new ApplicationSearchRequest();
        var respondentId = "abc";
        var result = await _controller.GetPaginatedApplicationsByRespondent(searchQuery, respondentId, pageSize: pageSize);

        var badRequest = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequest.Value.ShouldBe("pageSize must be greater than 0 if specified.");
    }
}