using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationAnswers : TestServiceBase<RespondentController>
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
            .Setup(m => m.Send(It.IsAny<GetModificationAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationAnswers(modificationChangeId, projectRecordId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationAnswersQuery>
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
            .Setup(m => m.Send(It.IsAny<GetModificationAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationAnswers(modificationChangeId, projectRecordId, categoryId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationAnswersQuery>
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