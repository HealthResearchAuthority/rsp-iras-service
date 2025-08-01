﻿using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class CreateApplication : TestServiceBase<ApplicationsController>
{
    [Theory, AutoData]
    public async Task CreateApplication_ShouldSendCommand_AndReturnCreatedApplication(ApplicationRequest request, ApplicationResponse mockResponse)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<CreateApplicationCommand>(c => c.CreateApplicationRequest == request), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await Sut.CreateApplication(request);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(mockResponse);

        // Verify
        mockMediator.Verify
        (
            m => m
            .Send
            (
                It.Is<CreateApplicationCommand>(c => c.CreateApplicationRequest == request),
                default
            ),
            Times.Once
        );
    }
}