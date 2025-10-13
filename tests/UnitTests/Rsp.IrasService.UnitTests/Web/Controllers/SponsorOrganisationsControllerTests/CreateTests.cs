using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

public class CreateTests : TestServiceBase
{
    private readonly SponsorOrganisationsController _controller;

    public CreateTests()
    {
        _controller = Mocker.CreateInstance<SponsorOrganisationsController>();
    }

    [Fact]
    public async Task Create_ShouldSendCommand_WithPassedDto()
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        var inputDto = new SponsorOrganisationDto { RtsId = "12345" };
        var returnedDto = new SponsorOrganisationDto { RtsId = "12345" };

        mockMediator
            .Setup(m => m.Send(It.IsAny<CreateSponsorOrganisationCommand>(), default))
            .ReturnsAsync(returnedDto);

        // Act
        var result = await _controller.Create(inputDto);


        Assert.Same(returnedDto, result);
    }
}