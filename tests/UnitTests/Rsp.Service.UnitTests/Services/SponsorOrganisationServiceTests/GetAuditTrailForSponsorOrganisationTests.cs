using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

public class GetAuditTrailForSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly RspContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public GetAuditTrailForSponsorOrganisationTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Theory, InlineAutoData(5)]
    public async Task EnableUserInSponsorOrganisation_Should_Set_IsActive_To_True(
        int records,
        Generator<SponsorOrganisationAuditTrail> generator)
    {
        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Seed users
        var users = await TestData.SeedData(_context, generator, records);
        var targetUser = users.First();
        await _context.SaveChangesAsync();

        // Act
        var result = await Sut.GetAuditTrailForSponsorOrganisation(targetUser.RtsId);

        // Assert
        result.ShouldNotBeNull();
    }
}