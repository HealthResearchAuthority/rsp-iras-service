using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.SponsorOrganisationServiceTests;

public class AddSponsorOrganisationUserTests : TestServiceBase<SponsorOrganisationsService>

{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public AddSponsorOrganisationUserTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Theory]
    [AutoData]
    public async Task Creates_SponsorOrganisation_Correctly(SponsorOrganisationUserDto sponsorOrganisationUserDto)
    {
        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Act
        var result = await Sut.AddUserToSponsorOrganisation(sponsorOrganisationUserDto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<SponsorOrganisationUserDto>();
    }
}