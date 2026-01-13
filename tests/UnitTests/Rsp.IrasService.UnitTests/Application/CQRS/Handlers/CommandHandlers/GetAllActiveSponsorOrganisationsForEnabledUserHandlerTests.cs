using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class GetAllActiveSponsorOrganisationsForEnabledUserHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Delegates_To_SponsorOrganisationsService(Guid userId)
    {
        // Arrange
        var sponsorOrganisationsService = new Mock<ISponsorOrganisationsService>();
        var handler = new GetAllActiveSponsorOrganisationsForEnabledUserHandler(sponsorOrganisationsService.Object);
        var cmd = new GetAllActiveSponsorOrganisationsForEnabledUserCommand(userId);

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        sponsorOrganisationsService.Verify(s =>
            s.GetSponsorOrganisationsForUser(
                It.Is<Guid>(u => u != Guid.Empty)),
            Times.Once);
    }
}