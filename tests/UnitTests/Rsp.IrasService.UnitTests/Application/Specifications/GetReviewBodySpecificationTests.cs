using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetReviewBodySpecificationTests
{
    [Theory, AutoData]
    public void GetRespondentApplicationSpecification_ById_ReturnsCorrectSpecification(
        Generator<ReviewBody> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();

        var spec = new GetReviewBodySpecification();

        // Act
        var result = spec
            .Evaluate(applications)
            .FirstOrDefault();

        // Assert
        result.ShouldNotBeNull();
    }
}