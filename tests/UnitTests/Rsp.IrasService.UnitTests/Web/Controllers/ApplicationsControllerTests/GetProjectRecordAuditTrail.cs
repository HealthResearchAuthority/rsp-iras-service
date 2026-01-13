using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetProjectRecordAuditTrail : TestServiceBase<ApplicationsController>
{
    [Theory, AutoData]
    public async Task GetProjectRecordAuditTrail_ShouldReturnOk_WhenAuditTrailExists
    (
        string projectRecordId,
        ProjectRecordAuditTrailResponse mockResponse
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send
            (
                It.Is<GetProjectRecordAuditTrailQuery>
                (
                    q => q.ProjectRecordId == projectRecordId
                ),
                default
            ))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.GetAuditTrailResponse(projectRecordId);

        // Assert
        result.ShouldBeEquivalentTo(mockResponse);
    }
}