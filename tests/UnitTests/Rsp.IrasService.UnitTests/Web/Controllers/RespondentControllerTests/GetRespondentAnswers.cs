using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetRespondentAnswersTests : TestServiceBase
{
    private readonly RespondentController _controller;

    public GetRespondentAnswersTests()
    {
        _controller = Mocker.CreateInstance<RespondentController>();
    }

    [Theory]
    [AutoData]
    public async Task GetRespondentAnswers_ByApplicationId_ShouldSendQuery(string applicationId,
        List<RespondentAnswerDto> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetResponsesQuery>(q => q.ApplicationId == applicationId), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetRespondentAnswers(applicationId);

        // Assert
        result.ShouldBe(mockResponse);
        mockMediator.Verify(m => m.Send(It.Is<GetResponsesQuery>(q => q.ApplicationId == applicationId), default),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetRespondentAnswers_ByApplicationIdAndCategoryId_ShouldSendQuery(string applicationId,
        string categoryId, List<RespondentAnswerDto> mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetResponsesQuery>(q => q.ApplicationId == applicationId && q.CategoryId == categoryId), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetRespondentAnswers(applicationId, categoryId);

        // Assert
        result.ShouldBe(mockResponse);
        mockMediator.Verify(
            m => m.Send(It.Is<GetResponsesQuery>(q => q.ApplicationId == applicationId && q.CategoryId == categoryId),
                default), Times.Once);
    }
}