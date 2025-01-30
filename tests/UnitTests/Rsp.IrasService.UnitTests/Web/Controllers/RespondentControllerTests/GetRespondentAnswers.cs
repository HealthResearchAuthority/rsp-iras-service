namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

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
        Assert.Equal(mockResponse, result);
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
        Assert.Equal(mockResponse, result);
        mockMediator.Verify(
            m => m.Send(It.Is<GetResponsesQuery>(q => q.ApplicationId == applicationId && q.CategoryId == categoryId),
                default), Times.Once);
    }
}