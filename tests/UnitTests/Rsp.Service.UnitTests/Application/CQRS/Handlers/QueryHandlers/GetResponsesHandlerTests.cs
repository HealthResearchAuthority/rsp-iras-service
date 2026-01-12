using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetResponsesHandlerTests
{
    private readonly GetResponsesHandler _handler;
    private readonly Mock<IRespondentService> _respondentServiceMock;

    public GetResponsesHandlerTests()
    {
        _respondentServiceMock = new Mock<IRespondentService>();
        _handler = new GetResponsesHandler(_respondentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetResponsesQueryWithoutCategoryId_ShouldCallGetResponsesOnce()
    {
        // Arrange
        var applicationId = "App-123";
        var expectedResponses = new List<RespondentAnswerDto>
        {
            new() { QuestionId = "Q1", AnswerText = "Yes" }
        };

        var query = new GetResponsesQuery { ApplicationId = applicationId };

        _respondentServiceMock
            .Setup(service => service.GetResponses(applicationId))
            .ReturnsAsync(expectedResponses);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldHaveSingleItem();
        _respondentServiceMock.Verify(service => service.GetResponses(applicationId), Times.Once);
    }

    [Fact]
    public async Task Handle_GetResponsesQueryWithCategoryId_ShouldCallGetResponsesWithCategoryOnce()
    {
        // Arrange
        var applicationId = "App-123";
        var categoryId = "C1";
        var expectedResponses = new List<RespondentAnswerDto>
        {
            new() { QuestionId = "Q1", AnswerText = "Yes", CategoryId = categoryId }
        };

        var query = new GetResponsesQuery { ApplicationId = applicationId, CategoryId = categoryId };

        _respondentServiceMock
            .Setup(service => service.GetResponses(applicationId, categoryId))
            .ReturnsAsync(expectedResponses);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldHaveSingleItem();
        _respondentServiceMock.Verify(service => service.GetResponses(applicationId, categoryId), Times.Once);
    }
}