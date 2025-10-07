using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetSponsorOrganisationSpecificationTests
{
    [Theory, AutoData]
    public void GetSponsorOrganisationSpecification_ById_ReturnsCorrectSpecification(
        Generator<SponsorOrganisation> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();

        var spec = new GetSponsorOrganisationsSpecification(1, 10, nameof(SponsorOrganisationDto.RtsId), "asc", null);

        // Act
        var result = spec
            .Evaluate(applications)
            .FirstOrDefault();

        // Assert
        result.ShouldNotBeNull();
    }
}