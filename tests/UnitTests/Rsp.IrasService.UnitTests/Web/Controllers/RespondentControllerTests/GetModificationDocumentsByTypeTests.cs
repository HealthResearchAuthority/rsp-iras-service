using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationDocumentsByTypeTests : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationDocumentsByType_ByDocumentId_ReturnsExpected
    (
        string projectRecordId,
        string documentTypeId,
        IEnumerable<ModificationDocumentDto> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationDocumentsByTypeQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationDocumentsByType(projectRecordId, documentTypeId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationDocumentsByTypeQuery>
                    (q =>
                        q.ProjectRecordId == projectRecordId &&
                        q.DocumentTypeId == documentTypeId
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}