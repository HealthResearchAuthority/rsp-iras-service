using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationChangeAnswers : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationAnswers_ByChangeIdAndRecordId_ReturnsExpected
    (
        Guid modificationChangeId,
        string projectRecordId,
        List<RespondentAnswerDto> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationChangeAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationChangeAnswers(modificationChangeId, projectRecordId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationChangeAnswersQuery>
                    (q =>
                        q.ProjectModificationChangeId == modificationChangeId &&
                        q.ProjectRecordId == projectRecordId &&
                        q.CategoryId == null
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }

    [Theory, AutoData]
    public async Task GetModificationAnswers_ByChangeIdRecordIdAndCategoryId_ReturnsExpected
    (
        Guid modificationChangeId,
        string projectRecordId,
        string categoryId,
        List<RespondentAnswerDto> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationChangeAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationChangeAnswers(modificationChangeId, projectRecordId, categoryId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationChangeAnswersQuery>
                    (q =>
                        q.ProjectModificationChangeId == modificationChangeId &&
                        q.ProjectRecordId == projectRecordId &&
                        q.CategoryId == categoryId
                    ),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}