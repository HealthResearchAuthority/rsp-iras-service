using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationDocumentAnswersTests : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationDocumentAnswers_ByDocumentId_ReturnsExpected
    (
        Guid documentId,
        IEnumerable<ModificationDocumentAnswerDto> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationDocumentAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationDocumentAnswers(documentId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationDocumentAnswersQuery>
                    (q =>
                        q.Id == documentId
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}