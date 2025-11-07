using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetModificationAuditTrail : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task GetModificationAuditTrail_ShouldReturnOk_WhenAuditTrailExists
    (
        Guid modificationId,
        ModificationAuditTrailResponse mockResponse
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send
            (
                It.Is<GetModificationAuditTrailQuery>
                (
                    q => q.ProjectModificationId == modificationId
                ),
                default
            ))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.GetModificationAuditTrail(modificationId);

        // Assert
        result.ShouldBeEquivalentTo(mockResponse);
    }
}