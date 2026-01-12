using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

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
        SponsorAuthorisationsModificationsSearchRequest searchQuery,
        ModificationSearchResponse mockResponse,
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
        var searchQuery = new SponsorAuthorisationsModificationsSearchRequest();
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