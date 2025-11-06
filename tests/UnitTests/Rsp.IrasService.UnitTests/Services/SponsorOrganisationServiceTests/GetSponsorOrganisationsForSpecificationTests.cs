using Ardalis.Specification;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.SponsorOrganisationServiceTests;

public class SponsorOrganisationsServiceTests : TestServiceBase<SponsorOrganisationsService>
{
    [Theory, AutoData]
    public async Task GetSponsorOrganisationsForSpecification_ReturnsMappedDtos
    (
        List<SponsorOrganisation> sponsorOrganisations
    )
    {
        // Arrange
        var spec = new TestSpecification();
        Mocker.GetMock<ISponsorOrganisationsRepository>()
                    .Setup(r => r.GetSponsorOrganisations(spec))
                    .ReturnsAsync(sponsorOrganisations);

        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Act
        var result = await Sut.GetSponsorOrganisationsForSpecification(spec);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(sponsorOrganisations.Count);
    }
}

public class TestSpecification : Specification<SponsorOrganisation>
{ }