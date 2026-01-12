using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

public class UpdateUserInSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly RspContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public UpdateUserInSponsorOrganisationTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Theory, InlineAutoData(5)]
    public async Task UpdateUserInSponsorOrganisation_Should_Update_Db(
        int records,
        Generator<SponsorOrganisationUser> generator)
    {
        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Seed users
        var users = await TestData.SeedData(_context, generator, records);
        await _context.SaveChangesAsync();

        var targetUser = users.First();

        // Act
        var result = await Sut.UpdateUserInSponsorOrganisation(new SponsorOrganisationUserDto
        {
            Id = targetUser.Id,
            RtsId = targetUser.RtsId,
            UserId = targetUser.UserId,
            IsAuthoriser = false,
            SponsorRole = "TestRole"
        });

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<SponsorOrganisationUserDto>();
        result.IsAuthoriser.ShouldBeFalse();
        result.SponsorRole.ShouldBe("TestRole");
    }
}