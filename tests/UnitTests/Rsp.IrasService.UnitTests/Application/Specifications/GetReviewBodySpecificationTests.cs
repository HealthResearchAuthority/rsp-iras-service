using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetReviewBodySpecificationTests
{
    [Theory, AutoData]
    public void GetRespondentApplicationSpecification_ById_ReturnsCorrectSpecification(
        Generator<RegulatoryBody> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();

        var spec = new GetReviewBodiesSpecification(1, 10, null);

        // Act
        var result = spec
            .Evaluate(applications)
            .FirstOrDefault();

        // Assert
        result.ShouldNotBeNull();
    }
}