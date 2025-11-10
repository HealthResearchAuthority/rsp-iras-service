using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetModificationsBySponsorOrganisationUserIdControllerTests : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public GetModificationsBySponsorOrganisationUserIdControllerTests()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetModificationsBySponsorOrganisationUserId_ShouldReturnOk_WhenModificationsExist(
        Guid sponsorOrganisationUserId,
        SponsorAuthorisationsSearchRequest searchQuery,
        ModificationResponse mockResponse,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetModificationsBySponsorOrganisationUserIdQuery>(q =>
                q.SponsorOrganisationUserId.Equals(sponsorOrganisationUserId) &&
                q.SearchQuery.Equals(searchQuery) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize &&
                q.SortField == sortField &&
                q.SortDirection == sortDirection), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetModificationsBySponsorOrganisationUserId(
            sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task GetModificationsBySponsorOrganisationUserId_ShouldReturnBadRequest_WhenPageParamsAreInvalid(
        int pageNumber, int pageSize)
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();
        var searchQuery = new SponsorAuthorisationsSearchRequest();
        var sortField = "SentToSponsorDate";
        var sortDirection = "asc";

        // Act
        var result = await _controller.GetModificationsBySponsorOrganisationUserId(
            sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        var badRequestResult = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.StatusCode.ShouldBe(400);
        badRequestResult.Value.ShouldBeOfType<string>();
    }
}