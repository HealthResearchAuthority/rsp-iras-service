using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.UnitTests.Application.Specifications;

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