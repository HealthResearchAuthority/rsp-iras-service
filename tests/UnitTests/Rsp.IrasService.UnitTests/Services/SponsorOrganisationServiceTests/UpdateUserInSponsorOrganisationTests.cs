using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.SponsorOrganisationServiceTests;

public class UpdateUserInSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public UpdateUserInSponsorOrganisationTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
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