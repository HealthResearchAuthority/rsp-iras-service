using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.UnitTests.Application.Specifications;

public class GetActiveSponsorOrganisationsForEnabledUserSpecificationTests
{
    [Theory, AutoData]
    public void Evaluate_Should_Return_Only_Active_Organisations_With_Active_User(Fixture fixture)
    {
        // Arrange
        var sponsorOrganisations = fixture.CreateMany<SponsorOrganisation>(30).ToList();
        var userId = Guid.NewGuid();

        for (int i = 0; i < sponsorOrganisations.Count; i++)
        {
            var orgUser = sponsorOrganisations[i].Users.FirstOrDefault();
            if (orgUser is not null)
            {
                if (i % 2 == 0) orgUser.UserId = userId;
                if (i % 3 == 0) orgUser.IsActive = true;
            }
        }

        var spec = new GetActiveSponsorOrganisationsForEnabledUserSpecification(userId);

        // Act
        var result = spec.Evaluate(sponsorOrganisations).ToList();

        // Assert
        var expected = sponsorOrganisations
            .Where(org => org.IsActive && org.Users.Any(u => u.UserId == userId && u.IsActive))
            .ToList();

        result.Count.ShouldBe(expected.Count);
        result.ShouldBeEquivalentTo(expected);
    }
}