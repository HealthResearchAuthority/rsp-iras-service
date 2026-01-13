using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetApplicationsByRespondent : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetApplicationsByRespondent()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetApplicationsByRespondent_ShouldReturnApplications_WhenDataExists(string respondentId,
        List<ApplicationResponse> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationsWithRespondentQuery>(q => q.RespondentId == respondentId), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplicationsByRespondent(respondentId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);
    }

    [Theory]
    [AutoData]
    public async Task GetApplicationsByRespondent_ShouldReturnEmptyList_WhenNoDataExists(string respondentId)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationsWithRespondentQuery>(q => q.RespondentId == respondentId), default))
            .ReturnsAsync(new List<ApplicationResponse>());

        // Act
        var result = await _controller.GetApplicationsByRespondent(respondentId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}