using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

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