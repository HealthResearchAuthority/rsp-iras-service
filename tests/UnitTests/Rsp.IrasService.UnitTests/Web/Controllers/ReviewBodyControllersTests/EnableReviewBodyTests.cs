using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ReviewBodyControllersTests;

public class EnableReviewBodyTests : TestServiceBase
{
    private readonly ReviewBodyController _controller;

    public EnableReviewBodyTests()
    {
        _controller = Mocker.CreateInstance<ReviewBodyController>();
    }

    [Theory]
    [AutoData]
    public async Task Enable_ShouldSendCommand(Guid id)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        // Act
        await _controller.Enable(id);

        // Assert
        mockMediator.Verify(
            m => m.Send(It.Is<EnableReviewBodyCommand>(c => c.ReviewBodyId == id), default), Times.Once);
    }

    [Theory, AutoData]
    public async Task Enable_ShouldSendCommand_AndLogAuditTrail_WhenReviewBodyIsEnabled(Guid id,
        ReviewBodyDto reviewBodyDto, IEnumerable<ReviewBodyAuditTrailDto> reviewBodyAuditTrailDtos)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        var mockAuditService = Mocker.GetMock<IReviewBodyAuditTrailService>();
        var userId = "test@example.com"; // Simulated user email

        // Mock mediator to return a ReviewBodyDto when the command is sent
        mockMediator
            .Setup(m => m.Send(It.IsAny<EnableReviewBodyCommand>(), default))
            .ReturnsAsync(reviewBodyDto);

        // Mock user email
        SetupHttpContextWithUserEmail(userId); // <-- helper you should define to simulate user

        // Mock auditService.GenerateAuditTrailDtoFromReviewBody to return a known audit dto
        mockAuditService
            .Setup(s => s.GenerateAuditTrailDtoFromReviewBody(reviewBodyDto, userId, ReviewBodyAuditTrailActions.Enable,
                reviewBodyDto))
            .Returns(reviewBodyAuditTrailDtos);

        // Act
        var result = await _controller.Enable(id);

        // Assert mediator was called
        mockMediator.Verify(
            m => m.Send(It.Is<EnableReviewBodyCommand>(c => c.ReviewBodyId == id), default),
            Times.Once);

        // Assert audit trail was generated and logged
        mockAuditService.Verify(
            s => s.GenerateAuditTrailDtoFromReviewBody(It.IsAny<ReviewBodyDto>(), userId,
                ReviewBodyAuditTrailActions.Enable, It.IsAny<ReviewBodyDto>()),
            Times.Once);
        mockAuditService.Verify(
            s => s.LogRecords(It.IsAny<IEnumerable<ReviewBodyAuditTrailDto>>()),
            Times.Once);

        result.ShouldBe(reviewBodyDto);
    }

    private void SetupHttpContextWithUserEmail(string email)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Email, email)
        }, "mock"));

        var httpContext = new DefaultHttpContext
        {
            User = user
        };

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }
}