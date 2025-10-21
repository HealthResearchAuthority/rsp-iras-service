using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.SponsorOrganisationServiceTests;

public class EnableSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public EnableSponsorOrganisationTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }


    [Theory]
    [InlineAutoData(5)]
    public async Task EnableSponsorOrganisation_Should_Set_IsActive_To_True(
        int records,
        Generator<SponsorOrganisation> generator)
    {
        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Seed users
        var users = await TestData.SeedData(_context, generator, records);
        var targetUser = users.First();
        await _context.SaveChangesAsync();

        // Act
        var result = await Sut.EnableSponsorOrganisation(targetUser.RtsId);

        // Assert
        result.ShouldNotBeNull();
        result.IsActive.ShouldBeTrue();
    }
}