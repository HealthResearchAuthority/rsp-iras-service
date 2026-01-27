using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.UnitTests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingHandlerTests
    : TestServiceBase<GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingHandler>
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnExpectedProjectClosuresSearchResponse_WithoutPaging(
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
                Status = ProjectClosureStatus.WithSponsor,
                IrasId = 123
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProjectRecordId = "PR-1002",
                ShortProjectTitle = "Beta Study",
                Status = ProjectClosureStatus.Authorised,
                IrasId = 456
            }
        };

        var expectedResponse = new ProjectClosuresSearchResponse
        {
            ProjectClosures = expectedClosures,
            TotalCount = expectedClosures.Count
        };

        var query = new GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery(
            sponsorOrganisationUserId,
            searchQuery
        );

        var closureService = Mocker.GetMock<IProjectClosureService>();

        closureService
            .Setup(service => service.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging(
                query.SponsorOrganisationUserId,
                query.SearchQuery))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(expectedClosures.Count);
        result.ProjectClosures.ShouldNotBeNull();
        result.ProjectClosures.Count().ShouldBe(expectedClosures.Count);

        var first = result.ProjectClosures.First();
        first.ProjectRecordId.ShouldBe("PR-1001");
        first.ShortProjectTitle.ShouldBe("Alpha Study");
        first.Status.ShouldBe(ProjectClosureStatus.WithSponsor);
        first.IrasId.ShouldBe(123);

        closureService.Verify(service =>
            service.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging(
                query.SponsorOrganisationUserId,
                query.SearchQuery),
            Times.Once);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenServiceReturnsNoClosures_WithoutPaging(
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

        var query = new GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery(
            sponsorOrganisationUserId,
            searchQuery
        );

        var closureService = Mocker.GetMock<IProjectClosureService>();

        closureService
            .Setup(service => service.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging(
                query.SponsorOrganisationUserId,
                query.SearchQuery))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(0);
        result.ProjectClosures.ShouldBeEmpty();

        closureService.Verify(service =>
            service.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging(
                query.SponsorOrganisationUserId,
                query.SearchQuery),
            Times.Once);
    }
}