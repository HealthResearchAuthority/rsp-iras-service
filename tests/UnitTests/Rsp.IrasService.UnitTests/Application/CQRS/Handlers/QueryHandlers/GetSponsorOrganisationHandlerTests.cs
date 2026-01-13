using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetSponsorOrganisationHandlerTests
{
    private readonly Mock<ISponsorOrganisationsService> _mock;
    private readonly GetAllSponsorOrganisationsHandler _handler;

    public GetSponsorOrganisationHandlerTests()
    {
        _mock = new Mock<ISponsorOrganisationsService>();
        _handler = new GetAllSponsorOrganisationsHandler(_mock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsQuery_ShouldReturnListOfApplications()
    {
        // Arrange
        var expectedResponse = new IrasService.Application.DTOS.Responses.AllSponsorOrganisationsResponse()
        {
            SponsorOrganisations = new List<SponsorOrganisationDto>
                {
                    new() { RtsId = "App-123" },
                    new() { RtsId = "App-456" }
                },
            TotalCount = 2
        };

        var query = new GetSponsorOrganisationsQuery(1, 100, nameof(SponsorOrganisationDto.RtsId), "asc", null);

        _mock
            .Setup(service => service.GetSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.RtsId), "asc", null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(expectedResponse.TotalCount);

        _mock.Verify(service => service.GetSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.RtsId), "asc", null), Times.Once);
    }
}