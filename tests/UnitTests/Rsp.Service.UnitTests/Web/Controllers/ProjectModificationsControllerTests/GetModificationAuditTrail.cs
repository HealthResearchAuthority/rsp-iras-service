using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

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