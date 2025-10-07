using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

/// <summary>
///     Covers the tests for GetReviewBodies method
/// </summary>
public class GetSponsorOrganisationsTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;
    private readonly IrasContext _context;

    public GetSponsorOrganisationsTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Theory, InlineAutoData(5)]
    public async Task Returns_Correct_SponsorOrganisations(int records, Generator<SponsorOrganisation> generator)
    {
        // Arrange
        Mocker.Use<ISponsorOrganisationsRepository>(_sponsorOrganisationRepository);
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);

        // Act
        var result = await Sut.GetSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.SponsorOrganisationName), "asc", null);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(records);
    }
}