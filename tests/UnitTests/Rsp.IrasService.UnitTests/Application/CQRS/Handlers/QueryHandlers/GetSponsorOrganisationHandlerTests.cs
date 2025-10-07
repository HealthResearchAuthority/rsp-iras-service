using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

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
                    new() { SponsorOrganisationName = "App-123" },
                    new() { SponsorOrganisationName = "App-456" }
                },
            TotalCount = 2
        };

        var query = new GetSponsorOrganisationsQuery(1, 100, nameof(SponsorOrganisationDto.SponsorOrganisationName), "asc", null);

        _mock
            .Setup(service => service.GetSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.SponsorOrganisationName), "asc", null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(expectedResponse.TotalCount);

        _mock.Verify(service => service.GetSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.SponsorOrganisationName), "asc", null), Times.Once);
    }
}