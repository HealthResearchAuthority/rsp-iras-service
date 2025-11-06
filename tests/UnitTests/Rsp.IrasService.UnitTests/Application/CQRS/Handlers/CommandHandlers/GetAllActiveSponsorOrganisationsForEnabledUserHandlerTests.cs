using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.Specifications;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

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
            s.GetSponsorOrganisationsForSpecification(
                It.Is<GetActiveSponsorOrganisationsForEnabledUserSpecification>(spec =>
                    spec != null
                )),
            Times.Once);
    }
}