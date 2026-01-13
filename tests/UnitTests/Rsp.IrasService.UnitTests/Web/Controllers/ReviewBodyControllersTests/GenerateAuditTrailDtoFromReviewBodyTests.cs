using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class GenerateAuditTrailDtoFromReviewBodyTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public GenerateAuditTrailDtoFromReviewBodyTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Theory,AutoData]
    public async Task GetAuditTrailForReviewBody_ShouldReturnAuditTrailResponse(Guid id, int skip, int take,
        ReviewBodyAuditTrailResponse expectedResponse)
    {
        // Arrange
        var mockAuditService = Mocker.GetMock<IReviewBodyAuditTrailService>();

        mockAuditService
            .Setup(s => s.GetAuditTrailForReviewBody(id, skip, take))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetAuditTrailForReviewBody(id, skip, take);

        // Assert
        result.ShouldBe(expectedResponse);
        mockAuditService.Verify(s => s.GetAuditTrailForReviewBody(id, skip, take), Times.Once);
    }
}