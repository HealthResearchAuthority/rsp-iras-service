using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsBySponsorOrganisationUserIdHandlerTests
{
    private readonly Mock<IProjectModificationService> _modificationServiceMock;
    private readonly GetModificationsBySponsorOrganisationUserIdHandler _handler;

    public GetModificationsBySponsorOrganisationUserIdHandlerTests()
    {
        _modificationServiceMock = new Mock<IProjectModificationService>();
        _handler = new GetModificationsBySponsorOrganisationUserIdHandler(_modificationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnExpectedModificationResponse()
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();
        var searchQuery = new SponsorAuthorisationsSearchRequest
        {
            SearchTerm = "IRAS-123"
        };

        var expectedModifications = new List<ModificationDto>
        {
            new() { ModificationId = "MOD-001", ChiefInvestigator = "Dr. Smith" }
        };

        var expectedResponse = new ModificationResponse
        {
            Modifications = expectedModifications,
            TotalCount = expectedModifications.Count
        };

        var query = new GetModificationsBySponsorOrganisationUserIdQuery(
            sponsorOrganisationUserId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: "SentToSponsorDate",
            sortDirection: "asc"
        );

        _modificationServiceMock
            .Setup(service => service.GetModificationsBySponsorOrganisationUserId(
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.Modifications.ShouldHaveSingleItem();
        result.Modifications.First().ModificationId.ShouldBe("MOD-001");

        _modificationServiceMock.Verify(service =>
            service.GetModificationsBySponsorOrganisationUserId(
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection),
            Times.Once);
    }
}