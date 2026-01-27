using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.UnitTests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectClosureControllerTests;

public class GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingTests
    : TestServiceBase<ProjectClosureController>
{
    [Theory]
    [AutoData]
    public async Task GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging_ShouldReturnOk_WhenClosuresExist(
        Guid sponsorOrganisationUserId,
        ProjectClosuresSearchRequest searchQuery,
        ProjectClosuresSearchResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        mockMediator
            .Setup(m => m.Send(
                It.Is<GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery>(q =>
                    q.SponsorOrganisationUserId.Equals(sponsorOrganisationUserId) &&
                    q.SearchQuery.Equals(searchQuery)),
                default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging(
            sponsorOrganisationUserId, searchQuery);

        // Assert
        result.Value.ShouldBe(mockResponse);

        mockMediator.Verify(m => m.Send(
            It.Is<GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery>(q =>
                q.SponsorOrganisationUserId.Equals(sponsorOrganisationUserId) &&
                q.SearchQuery.Equals(searchQuery)),
            default),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging_ShouldReturnEmpty_WhenServiceReturnsEmpty(
        Guid sponsorOrganisationUserId,
        ProjectClosuresSearchRequest searchQuery)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        var emptyResponse = new ProjectClosuresSearchResponse
        {
            ProjectClosures = Enumerable.Empty<ProjectClosureResponse>(),
            TotalCount = 0
        };

        mockMediator
            .Setup(m => m.Send(
                It.Is<GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery>(q =>
                    q.SponsorOrganisationUserId.Equals(sponsorOrganisationUserId) &&
                    q.SearchQuery.Equals(searchQuery)),
                default))
            .ReturnsAsync(emptyResponse);

        // Act
        var result = await Sut.GetProjectClosuresBySponsorOrganisationUserIdWithoutPaging(
            sponsorOrganisationUserId, searchQuery);

        // Assert
        result.Value.ShouldNotBeNull();
        result.Value.TotalCount.ShouldBe(0);
        result.Value.ProjectClosures.ShouldBeEmpty();

        mockMediator.Verify(m => m.Send(
            It.Is<GetProjectClosuresBySponsorOrganisationUserIdWithoutPagingQuery>(q =>
                q.SponsorOrganisationUserId.Equals(sponsorOrganisationUserId) &&
                q.SearchQuery.Equals(searchQuery)),
            default),
            Times.Once);
    }
}