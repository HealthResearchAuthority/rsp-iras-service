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