using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

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
        var result = await Sut.GetSponsorOrganisations(1, 100, nameof(SponsorOrganisationDto.RtsId), "asc", null);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(records);
    }
}