using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetDocumentTypes : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetListOfDocumentTypes
    (
        List<DocumentTypeResponse> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetDocumentTypesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetDocumentTypes();

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.IsAny<GetDocumentTypesQuery>(), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}