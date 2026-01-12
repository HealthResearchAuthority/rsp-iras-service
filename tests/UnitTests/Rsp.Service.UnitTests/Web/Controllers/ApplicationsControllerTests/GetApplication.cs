using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetApplication : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetApplication()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_ShouldReturnOk_WhenApplicationExists(string applicationId,
        ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationQuery>(q => q.ApplicationId == applicationId), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplication(applicationId);

        // Assert
        var okResult = result.Result.ShouldBeOfType<OkObjectResult>();
        okResult.StatusCode.ShouldBe(200);
        okResult.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_ShouldReturnNotFound_WhenApplicationDoesNotExist(string applicationId)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetApplicationQuery>(q => q.ApplicationId == applicationId), default))
            .ReturnsAsync((ApplicationResponse)null);

        // Act
        var result = await _controller.GetApplication(applicationId);

        // Assert
        result.Result.ShouldBeOfType<NotFoundResult>();
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_WithStatus_ShouldReturnOk_WhenApplicationExists(string applicationId,
        string status, ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetApplicationWithStatusQuery>(q =>
                    q.ApplicationId == applicationId && q.ApplicationStatus == status), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetApplication(applicationId, status);

        // Assert
        var okResult = result.Result.ShouldBeOfType<OkObjectResult>();
        okResult.StatusCode.ShouldBe(200);
        okResult.Value.ShouldBe(mockResponse);
    }

    [Theory]
    [AutoData]
    public async Task GetApplication_WithStatus_ShouldReturnNotFound_WhenApplicationDoesNotExist(string applicationId,
        string status)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<GetApplicationWithStatusQuery>(q =>
                    q.ApplicationId == applicationId && q.ApplicationStatus == status), default))
            .ReturnsAsync((ApplicationResponse)null);

        // Act
        var result = await _controller.GetApplication(applicationId, status);

        // Assert
        result.Result.ShouldBeOfType<NotFoundResult>();
    }
}