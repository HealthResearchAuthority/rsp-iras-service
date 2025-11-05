using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.SponsorOrganisationServiceTests;

public class GetAllActiveSponsorOrganisationsForEnabledUserTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public GetAllActiveSponsorOrganisationsForEnabledUserTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Theory, AutoData]
    public async Task GetAllActiveSponsorOrganisationsForEnabledUser_Should_Return_Only_Active_Organisations_With_Active_User
    (        
        Fixture fixture
    )
    {
        List<SponsorOrganisation> sponsorOrganisations = fixture.CreateMany<SponsorOrganisation>(30).ToList();

        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        var userId = Guid.NewGuid();

        for (int i = 0; i < sponsorOrganisations.Count; i++)
        {
            var orgUser = sponsorOrganisations[i].Users.FirstOrDefault();
            if (i % 2 == 0 && orgUser is not null)
            {
                orgUser.UserId = userId;
                
            }
            if (i % 3 == 0 && orgUser is not null) 
            {
                orgUser.IsActive = true;
            }
        }

        _context.SponsorOrganisations.AddRange(sponsorOrganisations);
        await _context.SaveChangesAsync();

        // Act
        var result = await Sut.GetAllActiveSponsorOrganisationsForEnabledUser(userId);

        // Assert
        var expected = sponsorOrganisations
            .Where(org => org.IsActive &&
                          org.Users.Any(u => u.UserId == userId && u.IsActive))
            .ToList();

        result.Count().ShouldBe(expected.Count);
    }
}