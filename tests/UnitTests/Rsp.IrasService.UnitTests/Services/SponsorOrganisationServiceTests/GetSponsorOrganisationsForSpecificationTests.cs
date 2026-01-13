using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

public class SponsorOrganisationsServiceTests : TestServiceBase<SponsorOrganisationsService>
{
    [Theory, AutoData]
    public async Task GetSponsorOrganisationsForUser_ReturnsMappedDtos(
        Guid userId,
        List<SponsorOrganisation> sponsorOrganisations)
    {
        // Arrange
        Mocker.GetMock<ISponsorOrganisationsRepository>()
              .Setup(r => r.GetSponsorOrganisations(It.IsAny<GetActiveSponsorOrganisationsForEnabledUserSpecification>()))
              .ReturnsAsync(sponsorOrganisations);

        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Act
        var result = await Sut.GetSponsorOrganisationsForUser(userId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(sponsorOrganisations.Count);
    }
}