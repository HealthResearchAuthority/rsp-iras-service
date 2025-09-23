using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationDocumentDetailsTests : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationDocumentDetails_ByDocumentId_ReturnsExpected
    (
        Guid documentId,
        ModificationDocumentDto expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationDocumentDetailsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationDocumentDetails(documentId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationDocumentDetailsQuery>
                    (q =>
                        q.Id == documentId
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}