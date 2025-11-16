using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsBySponsorOrganisationUserIdHandlerTests : TestServiceBase<GetModificationsBySponsorOrganisationUserIdHandler>
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnExpectedModificationSearchResponse
    (
        SponsorAuthorisationsSearchRequest searchQuery
    )
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();

        var expectedModifications = new List<ModificationDto>
        {
            new() { ModificationId = "MOD-001", ChiefInvestigator = "Dr. Smith" }
        };

        var expectedResponse = new ModificationSearchResponse
        {
            Modifications = expectedModifications,
            TotalCount = expectedModifications.Count
        };

        var query = new GetModificationsBySponsorOrganisationUserIdQuery
        (
            sponsorOrganisationUserId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: "SentToSponsorDate",
            sortDirection: "asc"
        );

        var modificationService = Mocker.GetMock<IProjectModificationService>();

        modificationService
            .Setup(service => service.GetModificationsBySponsorOrganisationUserId
            (
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection)
            ).ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.Modifications.ShouldHaveSingleItem();
        result.Modifications.First().ModificationId.ShouldBe("MOD-001");

        modificationService.Verify
        (
            service =>
                service.GetModificationsBySponsorOrganisationUserId
                (
                    query.SponsorOrganisationUserId,
                    query.SearchQuery,
                    query.PageNumber,
                    query.PageSize,
                    query.SortField,
                    query.SortDirection
                ),
            Times.Once
        );
    }
}