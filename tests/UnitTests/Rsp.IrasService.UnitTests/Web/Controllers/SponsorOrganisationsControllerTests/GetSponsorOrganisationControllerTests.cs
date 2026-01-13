using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class GetSponsorOrganisationControllerTests : TestServiceBase
{
    private readonly SponsorOrganisationsController _controller;

    public GetSponsorOrganisationControllerTests()
    {
        _controller = Mocker.CreateInstance<SponsorOrganisationsController>();
    }

    [Fact]
    public async Task GetReviewBodies_ShouldSendQuery()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.GetAllSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.RtsId),
            "asc", null);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.IsAny<GetSponsorOrganisationsQuery>(), default), Times.Once);
    }
}