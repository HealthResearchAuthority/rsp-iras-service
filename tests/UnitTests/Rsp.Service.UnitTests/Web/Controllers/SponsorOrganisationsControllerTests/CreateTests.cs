using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.SponsorOrganisationsControllerTests;

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