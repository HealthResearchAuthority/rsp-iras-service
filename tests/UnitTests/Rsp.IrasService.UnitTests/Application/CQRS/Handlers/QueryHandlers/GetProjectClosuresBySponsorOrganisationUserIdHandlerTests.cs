using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosuresBySponsorOrganisationUserIdHandlerTests
    : TestServiceBase<GetProjectClosuresBySponsorOrganisationUserIdHandler>
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnExpectedProjectClosuresSearchResponse(
        ProjectClosuresSearchRequest searchQuery
    )
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();

        var expectedClosures = new List<ProjectClosureResponse>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ProjectRecordId = "PR-1001",
                ShortProjectTitle = "Alpha Study",
                Status = "With sponsor",
                IrasId = 123
            }
        };

        var expectedResponse = new ProjectClosuresSearchResponse
        {
            ProjectClosures = expectedClosures,
            TotalCount = expectedClosures.Count
        };

        var query = new GetProjectClosuresBySponsorOrganisationUserIdQuery(
            sponsorOrganisationUserId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: nameof(ProjectClosureResponse.ShortProjectTitle),
            sortDirection: "asc"
        );

        var closureService = Mocker.GetMock<IProjectClosureService>();

        closureService
            .Setup(service => service.GetProjectClosuresBySponsorOrganisationUserId(
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.ProjectClosures.ShouldHaveSingleItem();
        result.ProjectClosures.First().ProjectRecordId.ShouldBe("PR-1001");
        result.ProjectClosures.First().ShortProjectTitle.ShouldBe("Alpha Study");
        result.ProjectClosures.First().Status.ShouldBe("With sponsor");
        result.ProjectClosures.First().IrasId.ShouldBe(123);

        closureService.Verify(service =>
            service.GetProjectClosuresBySponsorOrganisationUserId(
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection),
            Times.Once);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenServiceReturnsNoClosures(
        ProjectClosuresSearchRequest searchQuery
    )
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();

        var expectedResponse = new ProjectClosuresSearchResponse
        {
            ProjectClosures = Enumerable.Empty<ProjectClosureResponse>(),
            TotalCount = 0
        };

        var query = new GetProjectClosuresBySponsorOrganisationUserIdQuery(
            sponsorOrganisationUserId,
            searchQuery,
            pageNumber: 2,
            pageSize: 25,
            sortField: nameof(ProjectClosureResponse.IrasId),
            sortDirection: "desc"
        );

        var closureService = Mocker.GetMock<IProjectClosureService>();

        closureService
            .Setup(service => service.GetProjectClosuresBySponsorOrganisationUserId(
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(0);
        result.ProjectClosures.ShouldBeEmpty();

        closureService.Verify(service =>
            service.GetProjectClosuresBySponsorOrganisationUserId(
                query.SponsorOrganisationUserId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection),
            Times.Once);
    }
}