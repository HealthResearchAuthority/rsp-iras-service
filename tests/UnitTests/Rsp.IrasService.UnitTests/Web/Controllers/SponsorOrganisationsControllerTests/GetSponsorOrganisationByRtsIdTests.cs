using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class GetSponsorOrganisationByRtsIdTests : TestServiceBase
{
    private readonly SponsorOrganisationsController _controller;

    public GetSponsorOrganisationByRtsIdTests()
    {
        _controller = Mocker.CreateInstance<SponsorOrganisationsController>();
    }

    [Fact]
    public async Task GetSponsorOrganisationByRtsId_ShouldSendQuery_WithExpectedParameters()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        var rtsId = "111,222,333";
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetSponsorOrganisationsQuery>(), default))
            .ReturnsAsync(new AllSponsorOrganisationsResponse());

        // Act
        await _controller.GetSponsorOrganisationByRtsId(rtsId);

        // Assert
        mockMediator.Verify(
            m => m.Send(
                It.Is<GetSponsorOrganisationsQuery>(q =>
                    q.PageNumber == 1 &&
                    q.PageSize == int.MaxValue &&
                    q.SortField == "name" &&
                    q.SortDirection == "asc" &&
                    q.SearchQuery != null &&
                    q.SearchQuery.RtsIds != null &&
                    q.SearchQuery.RtsIds.Count == 3 &&
                    q.SearchQuery.RtsIds[0] == "111" &&
                    q.SearchQuery.RtsIds[1] == "222" &&
                    q.SearchQuery.RtsIds[2] == "333"
                ),
                default),
            Times.Once);
    }
}