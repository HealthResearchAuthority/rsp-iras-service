using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.SponsorOrganisationServiceTests;

/// <summary>
///     Covers the tests for GetReviewBodies method
/// </summary>
public class CreateSponsorOrganisationTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public CreateSponsorOrganisationTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Fact]
    public async Task CreateSponsorOrganisation_MapsDto_CallsRepo_ReturnsMappedDto()
    {
        // Arrange
        var mockRepo = Mocker.GetMock<ISponsorOrganisationsRepository>();
        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        var inputDto = new SponsorOrganisationDto
        {
            RtsId = "12345"
        };

        var createdEntity = new SponsorOrganisation
        {
            RtsId = "12345"
        };

        mockRepo
            .Setup(r => r.CreateSponsorOrganisation(
                It.Is<SponsorOrganisation>(e =>
                    e.RtsId == inputDto.RtsId)))
            .ReturnsAsync(createdEntity);

        // Act
        var result = await Sut.CreateSponsorOrganisation(inputDto);

        // Assert
        mockRepo.Verify(r => r.CreateSponsorOrganisation(
                It.Is<SponsorOrganisation>(e =>
                    e.RtsId == inputDto.RtsId)),
            Times.Once);

        result.ShouldNotBeNull();
        result.RtsId.ShouldBe(inputDto.RtsId);
    }
}