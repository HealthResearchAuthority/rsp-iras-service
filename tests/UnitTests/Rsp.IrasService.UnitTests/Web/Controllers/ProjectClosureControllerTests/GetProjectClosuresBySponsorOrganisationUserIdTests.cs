using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectClosureControllerTests;

public class GetProjectClosuresBySponsorOrganisationUserIdTests : TestServiceBase<ProjectClosureController>
{
    [Theory]
    [AutoData]
    public async Task GetProjectClosuresBySponsorOrganisationUserIdTests_ShouldReturnOk_WhenMProjectClosureExist(
        Guid sponsorOrganisationUserId,
        ProjectClosuresSearchRequest searchQuery,
        ProjectClosuresSearchResponse mockResponse,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetProjectClosuresBySponsorOrganisationUserIdQuery>(q =>
                q.SponsorOrganisationUserId.Equals(sponsorOrganisationUserId) &&
                q.SearchQuery.Equals(searchQuery) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize &&
                q.SortField == sortField &&
                q.SortDirection == sortDirection), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.GetProjectClosuresBySponsorOrganisationUserId(
            sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task GetProjectClosuresBySponsorOrganisationUserIdTests_ShouldReturnBadRequest_WhenPageParamsAreInvalid(
        int pageNumber, int pageSize)
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();
        var searchQuery = new ProjectClosuresSearchRequest();
        var sortField = "SentToSponsorDate";
        var sortDirection = "asc";

        // Act
        var result = await Sut.GetProjectClosuresBySponsorOrganisationUserId(
            sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        var badRequestResult = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.StatusCode.ShouldBe(400);
        badRequestResult.Value.ShouldBeOfType<string>();
    }
}