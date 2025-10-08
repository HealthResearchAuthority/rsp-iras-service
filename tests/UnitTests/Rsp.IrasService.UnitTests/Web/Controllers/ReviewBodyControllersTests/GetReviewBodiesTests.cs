using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class GetSponsorOrganisationTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public GetSponsorOrganisationTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Fact]
    public async Task GetReviewBodies_ShouldSendQuery()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.GetAllReviewBodies(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.IsAny<GetReviewBodiesQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetAllActiveReviewBodies_ShouldSendQuery_WithActiveStatus()
    {
        // Arrange
        var sortField = nameof(ReviewBodyDto.RegulatoryBodyName);
        const string sortDirection = "asc";
        var mockMediator = Mocker.GetMock<IMediator>();

        mockMediator
            .Setup(m => m.Send(It.IsAny<GetReviewBodiesQuery>(), default))
            .ReturnsAsync(new AllReviewBodiesResponse());

        // Act
        await _controller.GetAllActiveReviewBodies(sortField, sortDirection);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetReviewBodiesQuery>(q =>
                    q.PageNumber == 1 &&
                    q.PageSize == int.MaxValue &&
                    q.SortField == sortField &&
                    q.SortDirection == sortDirection &&
                    q.SearchQuery != null &&
                    q.SearchQuery.Status == true),
                default),
            Times.Once);
    }
}