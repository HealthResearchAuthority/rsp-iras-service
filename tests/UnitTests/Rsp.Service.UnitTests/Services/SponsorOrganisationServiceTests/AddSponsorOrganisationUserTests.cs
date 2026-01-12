using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

public class AddSponsorOrganisationUserTests : TestServiceBase<SponsorOrganisationsService>

{
    private readonly RspContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public AddSponsorOrganisationUserTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
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