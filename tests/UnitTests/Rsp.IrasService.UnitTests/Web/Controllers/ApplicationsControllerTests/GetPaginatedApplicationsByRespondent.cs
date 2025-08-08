using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

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
                            q.SearchQuery == null &&
                            q.PageIndex == 1 &&
                            q.PageSize == 5
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetPaginatedApplicationsByRespondent(respondentId, null, 1, 5);

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
                                q.SearchQuery == null &&
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
        var result = await _controller.GetPaginatedApplicationsByRespondent(respondentId, null, 1, 5);

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
        var respondentId = "abc";
        var result = await _controller.GetPaginatedApplicationsByRespondent(respondentId, pageIndex: pageIndex);

        var badRequest = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequest.Value.ShouldBe("pageIndex must be greater than 0 if specified.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public async Task GetPaginatedApplicationsByRespondent_ShouldReturnBadRequest_WhenPageSizeInvalid(int pageSize)
    {
        var respondentId = "abc";
        var result = await _controller.GetPaginatedApplicationsByRespondent(respondentId, pageSize: pageSize);

        var badRequest = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequest.Value.ShouldBe("pageSize must be greater than 0 if specified.");
    }
}