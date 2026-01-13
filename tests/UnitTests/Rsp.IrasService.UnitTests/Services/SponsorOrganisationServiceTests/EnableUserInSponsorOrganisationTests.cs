using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

public class EnableUserInSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public EnableUserInSponsorOrganisationTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }


    [Theory, InlineAutoData(5)]
    public async Task EnableUserInSponsorOrganisation_Should_Set_IsActive_To_True(
        int records,
        Generator<SponsorOrganisationUser> generator)
    {
        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Seed users
        var users = await TestData.SeedData(_context, generator, records);
        var targetUser = users.First();
        await _context.SaveChangesAsync();

        // Act
        var result = await Sut.EnableUserInSponsorOrganisation(targetUser.RtsId, targetUser.UserId);

        // Assert
        result.ShouldNotBeNull();
        result.IsActive.ShouldBeTrue();
    }

}